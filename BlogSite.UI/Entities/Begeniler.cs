namespace BlogSite.UI.Entities
{
    public class Begeni
    {
        public int Id { get; set; }
        public int MakaleId { get; set; }
        public string KullaniciId { get; set; } = null!;
        public ApplicationUser? Kullanici { get; set; }
        public Makale? Makale { get; set; }
    }
}
