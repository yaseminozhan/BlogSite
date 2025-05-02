using BlogSite.UI.Context;
using BlogSite.UI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BlogSite.UI.Controllers
{
    public class YazarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public YazarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //  Kullanıcı profil adresinden 
        public async Task<IActionResult> Detay(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var yazar = await _context.Users
                .Include(u => u.Makaleler)
                .FirstOrDefaultAsync(u => u.KullaniciUrl == id); // URL üzerinden erişim

            if (yazar == null)
                return NotFound();

            return View(yazar);
        }

        //  Giriş yapan kişinin kendi yazar sayfası
        [Authorize]
        public async Task<IActionResult> Profilim()
        {
            var user = await _userManager.GetUserAsync(User);

            var yazar = await _context.Users
                .Include(u => u.Makaleler)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (yazar == null)
                return NotFound();

            return View("Detay", yazar);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Duzenle()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ProfilDuzenleViewModel
            {
                AdSoyad = user.AdSoyad ?? "",
                Aciklama = user.Aciklama,
                MevcutFotoUrl = user.FotoUrl
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Duzenle(ProfilDuzenleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            user.AdSoyad = model.AdSoyad;
            user.Aciklama = model.Aciklama;

            if (model.YeniFoto != null)
            {
                var uzanti = Path.GetExtension(model.YeniFoto.FileName);
                var dosyaAdi = Guid.NewGuid() + uzanti;
                var yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                using var stream = new FileStream(yol, FileMode.Create);
                await model.YeniFoto.CopyToAsync(stream);

                user.FotoUrl = "/images/" + dosyaAdi;
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Profilim");
        }

    }
}
