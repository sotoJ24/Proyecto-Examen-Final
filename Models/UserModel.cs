using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto____Examen_Final.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        
        //Username is required and must be less than 1 character
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(255, ErrorMessage = "User name must be less than {1} characters.")]
        public string UserName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        // session ID is gonna be required 
        [Required(ErrorMessage = "SessionId is required.")]
        public int SessionId { get; set; }
      


    }
}
