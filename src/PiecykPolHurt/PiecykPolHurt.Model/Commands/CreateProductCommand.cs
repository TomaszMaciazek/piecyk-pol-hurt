﻿using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
