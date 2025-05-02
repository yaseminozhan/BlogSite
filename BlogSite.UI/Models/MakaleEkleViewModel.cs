using System.ComponentModel.DataAnnotations;
using BlogSite.UI.Entities;

public class MakaleEkleViewModel
{
    [Required]
    public string Baslik { get; set; } = string.Empty;

    [Required]
    public string Icerik { get; set; } = string.Empty;

    [Required]
    public int OkumaSuresi { get; set; }

    public List<int> SeciliKonuIdleri { get; set; } = new();

    public List<Konu> TumKonular { get; set; } = new(); // dropdown için
}
