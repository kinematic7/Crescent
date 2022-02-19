using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }          
        [NotMapped]
        public string ConfirmPassword { get; set; }

    }
}
