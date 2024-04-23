using System.ComponentModel.DataAnnotations;

namespace Proyecto____Examen_Final.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        //customer name is required
        
        [Required(ErrorMessage = "Customer name is required.")]
        public string ClientName { get; set; } = string.Empty;

         //Customer's last name is required

        [Required(ErrorMessage = "Customer's last name is required.")]
        public string ClientLastName { get; set; } = string.Empty;

         //Customer address is required

        [Required(ErrorMessage = "Customer address is required.")]
        public string Address { get; set; } = string.Empty;

        //The customer's phone number is required

        [Required(ErrorMessage = "The customer's phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        //Customer's email is required

        [Required(ErrorMessage = "Customer's email is required.")]
        [EmailAddress(ErrorMessage = "The email is invalid.")]
        public string Email { get; set; } = string.Empty;

        //The client password is required

        [Required(ErrorMessage = "The client password is required.")]
        public string ClientKey { get; set; } = string.Empty;
    }
}
