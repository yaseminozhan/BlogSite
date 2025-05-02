using BlogSite.UI.Entities;

namespace BlogSite.UI.Models
{
    public class UyeAnaSayfaViewModel
    {
        public List<int> TakipliKonuIdListesi { get; set; } = new();
        public List<Makale> TakipliMakaleler { get; set; } = new();
        public List<Makale> PopulerMakaleler { get; set; } = new();
    }
}
