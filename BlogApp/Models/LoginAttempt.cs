namespace BlogApp.Models
{
    public class LoginAttempt
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime LastFailedAttempt { get; set; }
    }
}
