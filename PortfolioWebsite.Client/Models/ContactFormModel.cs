
using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Client.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        [StringLength(1000, ErrorMessage = "Subject is too long.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        [StringLength(20000, ErrorMessage = "Message is too long.")]
        public string Message { get; set; }
    }
}
