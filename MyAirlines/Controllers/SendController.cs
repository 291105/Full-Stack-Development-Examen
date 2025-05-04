using Microsoft.AspNetCore.Mvc;
using MyAirlines.Extentions;
using MyAirlines.ViewModels;
using SendMail.Util.Mail.Interfaces;
using SendMail.Util.PDF.Interfaces;
using System.Security.Claims;

namespace MyAirlines.Controllers
{
    public class SendController : Controller
    {
        /*private readonly IEmailSend _emailSend;
        private readonly ICreatePDF _createPDF;
        private readonly IWebHostEnvironment _hostEnvironment;
        public SendController(IEmailSend emailSend, ICreatePDF createPDF, IWebHostEnvironment webHostEnvironment)
        {
            _emailSend = emailSend;
            _createPDF = createPDF;
            _hostEnvironment = webHostEnvironment;
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!sendEmailVM.Invoice)
                    {
                        string htmlMessage = @"
                        <!DOCTYPE html>
                        <html>
                        <head>
                          <meta charset='UTF-8'>
                          <style>
                            body { font-family: 'Segoe UI', sans-serif; background:#f4f4f4; margin:0; padding:0; }
                            .email-wrapper { width: 100%; background:#fff; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); overflow:hidden; }
                            .email-body h2 { color:#003366; }
                            .email-body p { font-size:16px; line-height:1.6; color:#333; margin: 0px; }
                            .booking-details { margin-top:20px; background:#f1f1f1; border-left:4px solid #003366; padding:25px; font-size:15px; }
                            .booking-details strong { display:inline-block; width:120px; }
                            .email-footer { text-align:center; font-size:12px; color:#777; padding:20px; }
                          </style>
                        </head>
                        <body>
                          <div class='email-wrapper'>
                            <div class='email-body'>
                              <h2>Hello,</h2>
                              <p>This is a confirmation email.</p>
                              <div class='booking-details'>
                                <p><strong>Booking ID:</strong> #123456</p>
                                <p><strong>Date:</strong> April 29, 2025</p>
                                <p><strong>Service:</strong> Premium Consultation</p>
                                <p><strong>Time:</strong> 2:00 PM – 3:00 PM</p>
                              </div>
                            </div>
                            <div class='email-footer'>
                              &copy; 2025 Avaro Airlines. All rights reserved.
                            </div>
                          </div>
                        </body>
                        </html>
                        ";

                        await _emailSend.SendEmailAsync(sendEmailVM.Email, sendEmailVM.FromName, htmlMessage);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string pdfFile = "factuur" + DateTime.Now.Year;
                        var pdfFileName = $"{pdfFile}_{Guid.NewGuid()}.pdf";
                        var products = new List<Product>
                        {
                            new Product { Name = "Product 1", Number=2, Price = 50 },
                            new Product { Name = "Product 2", Number=4, Price = 30 },
                        };
                        //het pad naar de map waarin het logo zich bevindt
                        string logoPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "avaro.png");

                        var pdfDocument = _createPDF.CreatePDFDocumentAsync(products, logoPath);

                        // Als de map pdf nog niet bestaat in de wwwroot map,
                        // maak deze dan aan voordat je het PDF-document opslaat.
                        string pdfFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "pdf");
                        Directory.CreateDirectory(pdfFolderPath);
                        //Combineer het pad naar de wwwroot map met het gewenste subpad en bestandsnaam voor het PDF-document.
                        string filePath = Path.Combine(pdfFolderPath, "example.pdf");
                        // Opslaan van de MemoryStream naar een bestand
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            pdfDocument.WriteTo(fileStream);
                        }
                        _emailSend.SendEmailAttachmentAsync(
                        sendEmailVM.Email,
                        "contact pagina",
                        sendEmailVM.Message,
                        pdfDocument,
                        pdfFileName
                        );
                    }
                    return View("Contact");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(sendEmailVM);
        }
    }*/
    }
}
