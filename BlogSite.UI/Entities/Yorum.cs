namespace BlogSite.UI.Entities
{
    public class Yorum
    {
        public int YorumId { get; set; }
        public string Icerik { get; set; } = null!;
        public DateTime Tarih { get; set; } = DateTime.Now;
        public int MakaleId { get; set; }
        public string KullaniciId { get; set; } = null!;

        public ApplicationUser? Kullanici { get; set; }
        public Makale? Makale { get; set; }
    }

}
