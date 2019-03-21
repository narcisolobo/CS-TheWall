using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWall.Models {
    public class Comment {

        [Key]
        public int CommentId { get; set; }

        public int MessageId { get; set; }

        public int UserId { get; set; }

        public Message Message { get; set; }

        public User User { get; set; }

        [Required (ErrorMessage = "Comment is required.")]
        [MinLength (5, ErrorMessage = "Comment must be at least 5 characters.")]
        [Display (Name = "Comment:")]
        public string CommentBody { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}