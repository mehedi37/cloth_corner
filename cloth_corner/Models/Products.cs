﻿using cloth_corner.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cloth_corner.Models
{
    public class Products
    {
        [Key]
        public required int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required string ProductImage { get; set; }
        public required decimal ProductPrice { get; set; }
        [ForeignKey("AppUser")]
        public required string UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public AppUser? AppUser { get; set; }
        public required int Stock { get; set; }
    }
}
