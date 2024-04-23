using System.ComponentModel.DataAnnotations;

namespace Proyecto____Examen_Final.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer's last name is required.")]
        public string ClientLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "The customer's phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer's email is required.")]
        [EmailAddress(ErrorMessage = "The email is invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The client password is required.")]
        public string ClientKey { get; set; } = string.Empty;
    }
}
