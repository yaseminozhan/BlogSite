namespace BlogSite.UI.Entities
{
    public class Makale
    {
        public int MakaleId { get; set; }
        public string Baslik { get; set; } = null!;
        public string Icerik { get; set; } = null!;
        public DateTime YayınTarihi { get; set; } = DateTime.Now;
        public int OkumaSuresi { get; set; } // dakika 
        public string YazarId { get; set; } = null!;
        // Navigation
        public ApplicationUser? Yazar { get; set; }
        public ICollection<MakaleKonu>? MakaleKonular { get; set; }
    }
}
