namespace BlogSite.UI.Entities
{
    public class Bildirim
    {
        public int Id { get; set; }
        public string KullaniciId { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public bool OkunduMu { get; set; } = false;
        public DateTime Tarih { get; set; } = DateTime.Now;

        public ApplicationUser? Kullanici { get; set; }
    }

}
