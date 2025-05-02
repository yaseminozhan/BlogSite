using BlogSite.UI.Context; 
using BlogSite.UI.Entities;
using BlogSite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel();

            if (!User.Identity.IsAuthenticated)
            {
                model.GirisYapildiMi = false;
                model.TumMakaleler = await _context.Makaleler
                    .Include(m => m.Yazar)
                    .OrderByDescending(m => m.YayýnTarihi)
                    .Take(10)
                    .ToListAsync();

                return View(model); //  Modeli gönderiyoruz
            }

            var user = await _userManager.GetUserAsync(User);

            var takipliKonular = await _context.TakipEdilenKonular
                .Where(x => x.KullaniciId == user.Id)
                .Select(x => x.Konu)
                .ToListAsync();

            var takipliMakaleler = await _context.MakaleKonular
                .Where(x => takipliKonular.Select(k => k.KonuId).Contains(x.KonuId))
                .Include(x => x.Makale)
                .ThenInclude(m => m!.Yazar)
                .Select(x => x.Makale!)
                .Distinct()
                .ToListAsync();

            model.GirisYapildiMi = true;
            model.TakipliKonular = takipliKonular;
            model.TakipliMakaleler = takipliMakaleler;
            model.PopulerMakaleler = await _context.Makaleler
                .OrderByDescending(x => x.MakaleKonular!.Count)
                .Take(5)
                .ToListAsync();
            model.KullaniciUrl = user.KullaniciUrl ?? "";

            return View(model); 
        }
        //Hem ziyaretçilere hem de üyelere açýk olacak.
        public IActionResult Hakkimizda()
        {
            return View();
        }


    }
}
