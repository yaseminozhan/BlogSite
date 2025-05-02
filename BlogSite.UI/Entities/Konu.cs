namespace BlogSite.UI.Entities
{
    public class Konu
    {
        public int KonuId { get; set; }
        public string KonuAdi { get; set; } = null!;

        // Navigation
        public ICollection<MakaleKonu>? MakaleKonular { get; set; }
    }
}
