namespace BlogSite.UI.Entities
{
    public class Mesaj
    {
        public int Id { get; set; }

        public string GonderenId { get; set; } = null!;
        public string AliciId { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public DateTime Tarih { get; set; } = DateTime.Now;

        public ApplicationUser? Gonderen { get; set; }
        public ApplicationUser? Alici { get; set; }
    }

}
