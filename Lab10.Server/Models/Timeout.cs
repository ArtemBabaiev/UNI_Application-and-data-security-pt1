namespace Lab10.Server.Models
{
    public class LoginTimeout
    {

        public long Id { get; set; }
        public DateTime? Expiration { get; set; }
        public int Tries { get; set; }

        public string? UserUsername { get; set; }
        public User User { get; set; }
    }
}
