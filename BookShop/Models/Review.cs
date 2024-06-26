﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book? Book { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string AppUser { get; set; }
        
        [Column(TypeName = "nvarchar(500)")]
        public string Comment { get; set; }
        public int? Rating { get; set; }
       
    }
}

