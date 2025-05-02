namespace BlogSite.UI.Entities
{
    public class TakipEdilenKonu
    {
        public int Id { get; set; }
        public string KullaniciId { get; set; } = null!;
        public int KonuId { get; set; }

        public ApplicationUser? Kullanici { get; set; }
        public Konu? Konu { get; set; }
    }

}
