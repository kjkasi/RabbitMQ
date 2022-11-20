﻿using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Catalog.Models
{
    public class CatalogCreateDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
