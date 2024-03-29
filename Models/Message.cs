using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models {
    public class Message {

        [Key]
        public int MessageId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required (ErrorMessage = "Message is required.")]
        [MinLength (5, ErrorMessage = "Message must be at least 5 characters.")]
        [Display (Name = "Message:")]
        public string MessageBody { get; set; }

        public List<Comment> ChildComments {get;set;} = new List<Comment>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}