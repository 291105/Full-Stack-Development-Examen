using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using QRCoder;
using FlightProject.Domain;
using System.Drawing;
using SendMail.Util.PDF.Interfaces;


using iText.Layout.Borders;
using FlightProject.Domain.Entities;
using FlightProject.Services.Interfaces;

namespace SendMail.Util.PDF
{
    public class CreatePDF : ICreatePDF
    {
        private readonly IClassService _classService;
        private readonly IMealService _mealService;
        private readonly ISeatService _seatService;

        public CreatePDF(IClassService classService, IMealService mealService, ISeatService seatService)
        {
            _classService = classService;
            _mealService = mealService;
            _seatService = seatService;
        }
        public async Task<MemoryStream> CreatePDFDocumentAsync(List<Ticket> tickets, Booking booking, string logoPath)

        {

            // Genereren van de PDF-factuur

            using (MemoryStream stream = new MemoryStream())
            {
                
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                // Top layout: logo left, QR code right
                Table headerTable = new Table(2).UseAllAvailableWidth();
                headerTable.SetMarginBottom(50);

                // Factuurinformatie toevoegen
                iText.Layout.Element.Image logo = new iText.Layout.Element.Image(ImageDataFactory.Create(logoPath)).ScaleToFit(75, 75);
                logo.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                string companyName = "Avaro";
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(companyName, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(3);
                iText.Layout.Element.Image qrCodeImageElement = new iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.RIGHT);

                headerTable.AddCell(new Cell().Add(logo).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                headerTable.AddCell(new Cell().Add(qrCodeImageElement).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                document.Add(new Paragraph("FlightTicket").SetFontSize(24).SetBold().SetTextAlignment(TextAlignment.CENTER).SetMarginBottom(20));


                document.Add(headerTable);
                document.Add(new Paragraph("Factuur").SetFontSize(20));
                document.Add(new Paragraph("Factuurnummer: 001").SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).SetFontSize(16).SetFontColor(ColorConstants.BLUE));
                document.Add(new Paragraph("Datum: " + DateTime.Now.ToShortDateString()));
                document.Add(new Paragraph(""));

                double totalPrice = 0;

                foreach (var ticket in tickets)
                {
                    Table ticketTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 })).UseAllAvailableWidth();
                    ticketTable.SetMarginBottom(20);
                    ticketTable.SetBorder(new SolidBorder(1));

                    ticketTable.AddCell("Naam: " + ticket.FirstName + " " + ticket.LastName);

                    ticketTable.AddCell("Rijksregisternummer: " + ticket.NationalRegisterNumber);
                    

                    ticketTable.AddCell("Vertrek: " + ticket.Departure + " " + ticket.DepartureTime + " "  + "✈️" + " " + ticket.Arrival + " " + ticket.ArrivalTime);

                    
                    var ticketClass = await _classService.getClassById(ticket.ClassId);
                    ticketTable.AddCell("Class:" + ticketClass.Name);

                    var ticketMeal = await _mealService.GetMealById(ticket.MealId);
                    ticketTable.AddCell("Meal: " + ticketMeal.Name);
                    
                    
                    ticketTable.AddCell("SeatNumber:" + await _seatService.GetSeatNumberBySeatId((int)ticket.SeatId));

                    ticketTable.AddCell("TicketPrice: " + ticket.Price);

                    document.Add(ticketTable);

                    
                }

                totalPrice = booking.TotalPricePerBooking;

                document.Add(new Paragraph($"Totaalbedrag: {totalPrice.ToString("C")}")
                .SetFontSize(16)
                .SetBold()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetMarginTop(10));

                document.Add(new Paragraph("On all the intermediate flights you will also see the ticketprice, don't worry! The ticketPrice is for the whole flight the intermediate flights are included! The totalprice at the bottom is what you actually paid!"));

                document.Close();
                return new MemoryStream(stream.ToArray());


            }
        }


        // This method is for converting bitmap into a byte array
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
