using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lab10.Server.Models
{
    public class User
    {
        [Key]
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
