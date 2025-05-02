using BlogSite.UI.Entities;

public class HomePageViewModel
{
    public bool GirisYapildiMi { get; set; }

    // Giriş yapmayan kullanıcıya gösterilir
    public List<Makale> TumMakaleler { get; set; } = new();

    // Giriş yapan kullanıcı için
    public List<Makale> TakipliMakaleler { get; set; } = new();
    public List<Makale> PopulerMakaleler { get; set; } = new();
    public List<Konu> TakipliKonular { get; set; } = new();
    public string KullaniciUrl { get; set; } = string.Empty;
}
