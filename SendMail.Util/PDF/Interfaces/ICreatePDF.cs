using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Util.PDF.Interfaces
{
    public interface ICreatePDF
    {
        Task<MemoryStream> CreatePDFDocumentAsync(List<Ticket> tickets, Booking booking, string logoPath);
    }
}
