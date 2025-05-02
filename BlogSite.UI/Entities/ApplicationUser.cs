using Microsoft.AspNetCore.Identity;

namespace BlogSite.UI.Entities
{
    public class ApplicationUser :IdentityUser
    {       
        public string? AdSoyad { get; set; }        
        public string? Aciklama { get; set; }        
        public string? FotoUrl { get; set; }        
        public string? KullaniciUrl { get; set; }
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public bool AktifMi { get; set; } = true;
        //  navigationlar
        public ICollection<Makale>? Makaleler { get; set; }
    }
}
