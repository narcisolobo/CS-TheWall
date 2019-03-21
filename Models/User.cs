using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models {
    public class User {

        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage = "First name is required.")]
        [MinLength (2, ErrorMessage = "First Name must be at least 2 characters.")]
        [Display (Name = "First Name:")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last name is required.")]
        [MinLength (2, ErrorMessage = "Last Name must be at least 2 characters.")]
        [Display (Name = "Last Name:")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display (Name = "Email Address:")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is required.")]
        [MinLength (8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType (DataType.Password)]
        [Display (Name = "Password:")]
        public string Password { get; set; }

        public List<Message> CreatedMessages { get; set; } = new List<Message> ();

        public List<Comment> CreatedComments { get; set; } = new List<Comment> ();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        [Compare ("Password")]
        [DataType (DataType.Password)]
        [Display (Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }
    }
}