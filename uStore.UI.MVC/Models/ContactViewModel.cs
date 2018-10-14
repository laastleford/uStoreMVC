using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace uStore.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }

        //[RegularExpression(@"Reg Ex goes ehre")]
        [Required(ErrorMessage = "* Required")]
        [EmailAddress(ErrorMessage = "* Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(25, ErrorMessage = "* Subject cannot exceed 25 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Message { get; set; }
    }
}