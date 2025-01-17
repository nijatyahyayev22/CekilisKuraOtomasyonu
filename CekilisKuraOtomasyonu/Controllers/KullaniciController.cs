using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

public class KullaniciController : Controller
{
    private readonly CekilisOtomasyonuDbContext _context;
    
    public KullaniciController(CekilisOtomasyonuDbContext context)
    {
        _context = context;
    }

    public IActionResult Giris() => View();
    public IActionResult Kayit() => View();

    [HttpPost]
    public IActionResult Kayit(Kullanici kullanici)
    {
        if (ModelState.IsValid)
        {
            _context.Kullanici.Add(kullanici);
            _context.SaveChanges();
            return RedirectToAction("Giris");
        }
        return View(kullanici);
    }

    [HttpPost]
    public async Task<IActionResult> Giris(string email, string sifre)
    {
        var kullanici = _context.Kullanici.FirstOrDefault(u => u.Email == email && u.Sifre == sifre);
        if (kullanici != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kullanici.AdSoyad),
                new Claim(ClaimTypes.Email, kullanici.Email),
                new Claim("KullaniciID", kullanici.KullaniciID.ToString())
            };

            var kimlik = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(kimlik);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Geçersiz email veya şifre.");
        return View();
    }

    public async Task<IActionResult> Cikis()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
