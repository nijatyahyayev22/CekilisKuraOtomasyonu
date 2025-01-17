using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class SonucController : Controller
{
    private readonly CekilisOtomasyonuDbContext _context;

    public SonucController(CekilisOtomasyonuDbContext context)
    {
        _context = context;
    }

    public IActionResult Listele()
    {
        var sonuclar = _context.Sonuc.ToList();
        return View(sonuclar);
    }

    public IActionResult Detay(int id)
    {
        var sonuc = _context.Sonuc.FirstOrDefault(s => s.SonucID == id);
        if (sonuc == null)
        {
            return NotFound();
        }
        return View(sonuc);
    }
}
