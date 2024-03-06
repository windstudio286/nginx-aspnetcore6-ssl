using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace NginxDocker.Api.Models
{
    [Table("user_tbl")]
    public partial class User
    {
        [Key]
        [Column("user_id")]
        [JsonPropertyName("user_id")] // Customize JSON key name
        public long UserId { get; set; }
        [Column("user_name")]
        [JsonPropertyName("user_name")]
        public string Username { get; set; } = null!;
        [Column("password")]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? Password { get; set; }
        [Column("refresh_token")]
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        [Column("refresh_token_expiry_time")]
        [JsonPropertyName("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
