namespace SamuraiApp.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; } //ref key to Samurai class
        public int SamuraiId { get; set; } //FK
    }
}