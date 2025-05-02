using BlogSite.UI.Context;
using BlogSite.UI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class MakaleController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MakaleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

 public async Task<IActionResult> Index()
    {
        var makaleler = await _context.Makaleler
            .Include(m => m.Yazar)
            .OrderByDescending(m => m.YayınTarihi)
            .ToListAsync();

        return View(makaleler);
    }

    [HttpGet]
    public async Task<IActionResult> Ekle()
    {
        var viewModel = new MakaleEkleViewModel
        {
            TumKonular = await _context.Konular.ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Ekle(MakaleEkleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.TumKonular = await _context.Konular.ToListAsync();
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);

        var makale = new Makale
        {
            Baslik = model.Baslik,
            Icerik = model.Icerik,
            OkumaSuresi = model.OkumaSuresi,
            YazarId = user.Id,
            YayınTarihi = DateTime.Now
        };

        _context.Makaleler.Add(makale);
        await _context.SaveChangesAsync();

        // Konu eşleştir
        foreach (var konuId in model.SeciliKonuIdleri)
        {
            _context.MakaleKonular.Add(new MakaleKonu
            {
                MakaleId = makale.MakaleId,
                KonuId = konuId
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Detay(int id)
    {
        var makale = await _context.Makaleler
            .Include(m => m.Yazar)
            .Include(m => m.MakaleKonular)
                .ThenInclude(mk => mk.Konu)
            .FirstOrDefaultAsync(m => m.MakaleId == id);

        if (makale == null)
            return NotFound();

        return View(makale);
    }


}
