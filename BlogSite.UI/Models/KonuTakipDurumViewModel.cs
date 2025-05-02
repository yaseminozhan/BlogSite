namespace BlogSite.UI.Models
{
    public class KonuTakipDurumViewModel
    {
        public int KonuId { get; set; }
        public string KonuAdi { get; set; } = string.Empty;
        public bool TakipEdiliyor { get; set; }
    }
}
