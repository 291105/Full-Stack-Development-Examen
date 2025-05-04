using System.ComponentModel.DataAnnotations;

namespace SendMail.ViewModels
{
    public class SendMailVM
    {
        [Required, Display(Name = "Jouw naam")]
        public string? FromName { get; set; }
        [Required, Display(Name = "Jouw e-mailadres"), EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string? Message { get; set; }
        public bool Invoice { get; set; }
    }
}
