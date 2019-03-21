using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models {
    public class LoginUser {

        [Required (ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display (Name = "Email Address:")]
        public string LoginEmail { get; set; }

        [Required (ErrorMessage = "Password is required.")]
        [MinLength (8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType (DataType.Password)]
        [Display (Name = "Password:")]
        public string LoginPassword { get; set; }
    }
}