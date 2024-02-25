using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NginxDocker.Api.Models
{
    [Table("product")]
    public partial class Product
    {
        [Key]
        [Column("product_id")]
        [JsonPropertyName("product_id")] // Customize JSON key name
        public int ProductId { get; set; }
        [Column("product_name")]
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = null!;
        [Column("product_price")]
        [JsonPropertyName("product_price")]
        public decimal? ProductPrice { get; set; }
        [Column("currency")]
        [JsonPropertyName("product_currency")]
        public string? Currency { get; set; }
    }
}
