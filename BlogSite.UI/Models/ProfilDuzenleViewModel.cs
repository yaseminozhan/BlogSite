using System.ComponentModel.DataAnnotations;

public class ProfilDuzenleViewModel
{
    [Required]
    public string AdSoyad { get; set; } = string.Empty;

    public string? Aciklama { get; set; }

    public IFormFile? YeniFoto { get; set; }

    public string? MevcutFotoUrl { get; set; }
}
