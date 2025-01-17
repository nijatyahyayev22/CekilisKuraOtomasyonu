using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class CekilisController : Controller
{
    private readonly CekilisOtomasyonuDbContext _context;

    public CekilisController(CekilisOtomasyonuDbContext context)
    {
        _context = context;
    }

    public IActionResult Ekle() => View();

    [HttpPost]
    public IActionResult Ekle(Cekilis cekilis)
    {
        if (ModelState.IsValid)
        {

            if (cekilis.Katilimcilar.Count < cekilis.KazananSayisi + cekilis.YedekSayisi)
            {
                ModelState.AddModelError("", "Yeterli katılımcı yok!");
                return View(cekilis);
            }

            cekilis.KullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID")?.Value);

            _context.Cekilis.Add(cekilis);
            _context.SaveChanges();

            var katilimcilar = _context.Katilimci
                .Where(k => k.CekilisID == cekilis.CekilisID)
                .ToList();

            var random = new Random();
            var secilenKatilimcilar = katilimcilar
                .OrderBy(_ => random.Next())
                .Take(cekilis.KazananSayisi + cekilis.YedekSayisi)
                .ToList();

            for (int i = 0; i < cekilis.KazananSayisi; i++)
            {
                var sonuc = new Sonuc
                {
                    CekilisID = cekilis.CekilisID,
                    KatilimciID = secilenKatilimcilar[i].KatilimciID,
                    SonucTipi = "Kazanan"
                };
                _context.Sonuc.Add(sonuc);
            }

            for (int i = cekilis.KazananSayisi; i < cekilis.KazananSayisi + cekilis.YedekSayisi; i++)
            {
                var sonuc = new Sonuc
                {
                    CekilisID = cekilis.CekilisID,
                    KatilimciID = secilenKatilimcilar[i].KatilimciID,
                    SonucTipi = "Yedek"
                };
                _context.Sonuc.Add(sonuc);
            }

            _context.SaveChanges();

            return RedirectToAction("Detay", new { id = cekilis.CekilisID });
        }

        return View(cekilis);
    }

    [HttpPost]
    public IActionResult Sil(int id)
    {
        var cekilis = _context.Cekilis.Find(id);
        if (cekilis == null)
        {
            return NotFound();
        }

        _context.Cekilis.Remove(cekilis);
        _context.SaveChanges();

        return RedirectToAction("Sonuclar");
    }

    public IActionResult Sonuclar()
    {
        var kullaniciID = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "KullaniciID").Value);
        var cekilisler = _context.Cekilis.Include(m=> m.Katilimcilar)
        .Where(c => c.KullaniciID == kullaniciID).ToList();
        return View(cekilisler);
    }
    public IActionResult Detay(int id)
    {
        var cekilis = _context.Cekilis
            .Include(c => c.Katilimcilar)
            .Include(c => c.Kullanici)
            .FirstOrDefault(c => c.CekilisID == id);

        if (cekilis == null)
        {
            return NotFound();
        }

        var sonuclar = _context.Sonuc
            .Where(s => s.CekilisID == id)
            .Include(s => s.Katilimci)
            .ToList();

        ViewBag.Sonuclar = sonuclar;

        return View(cekilis);
    }
}
