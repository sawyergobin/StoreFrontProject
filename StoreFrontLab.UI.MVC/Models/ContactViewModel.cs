using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreFrontLab.UI.MVC.Models
{
    public class ContactViewModel
    {

        //fields
        //N/A

        //properties
        [Required(ErrorMessage = "* Name is a required field")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }
        public string Subject { get; set; }

        [Required(ErrorMessage = "* A Message is required")]
        [UIHint("MultilineText")]
        public string Message { get; set; }

        [Required(ErrorMessage = "* Email Address is a required field")]
        [EmailAddress(ErrorMessage = "* Please ensure your email address is the correct format")]
        [Display(Name = "Your Email")]
        public string EmailAddress { get; set; }

        //ctors

        //methods

        //toString() Override
    }
}