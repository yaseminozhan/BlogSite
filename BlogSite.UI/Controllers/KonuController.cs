using BlogSite.UI.Context;
using BlogSite.UI.Entities;
using BlogSite.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.UI.Controllers
{
    public class KonuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KonuController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var konular = await _context.Konular.ToListAsync();
            var model = new List<KonuTakipDurumViewModel>();

            string? kullaniciId = null;
            if (User.Identity.IsAuthenticated)
                kullaniciId = _userManager.GetUserId(User);

            foreach (var konu in konular)
            {
                bool takipEdiliyor = false;

                if (kullaniciId != null)
                {
                    takipEdiliyor = await _context.TakipEdilenKonular
                        .AnyAsync(x => x.KullaniciId == kullaniciId && x.KonuId == konu.KonuId);
                }

                model.Add(new KonuTakipDurumViewModel
                {
                    KonuId = konu.KonuId,
                    KonuAdi = konu.KonuAdi,
                    TakipEdiliyor = takipEdiliyor
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TakipEt(int konuId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var kullaniciId = _userManager.GetUserId(User);

            var varMi = await _context.TakipEdilenKonular
                .AnyAsync(x => x.KullaniciId == kullaniciId && x.KonuId == konuId);

            if (!varMi)
            {
                _context.TakipEdilenKonular.Add(new TakipEdilenKonu
                {
                    KullaniciId = kullaniciId!,
                    KonuId = konuId
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TakibiBirak(int konuId)
        {
            var kullaniciId = _userManager.GetUserId(User);

            var takip = await _context.TakipEdilenKonular
                .FirstOrDefaultAsync(x => x.KullaniciId == kullaniciId && x.KonuId == konuId);

            if (takip != null)
            {
                _context.TakipEdilenKonular.Remove(takip);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
