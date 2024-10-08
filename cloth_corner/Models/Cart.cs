﻿using cloth_corner.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cloth_corner.Models
{
    public class Cart
    {
        [Key]
        public required int CartId { get; set; }

        [ForeignKey("AppUser")]
        public required string UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public AppUser? AppUser { get; set; }

        public ICollection<CartDetails> CartDetails { get; set; } = new List<CartDetails>();

        public bool IsPurchased { get; set; } = false;

    }
}
