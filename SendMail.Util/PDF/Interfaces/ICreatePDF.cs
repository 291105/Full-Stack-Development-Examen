using SendMail.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Util.PDF.Interfaces
{
    public interface ICreatePDF
    {
        MemoryStream CreatePDFDocumentAsync(List<Product> products, string logoPath);
    }
}
