using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalApp.Controllers
{
    public class GirisController : Controller
    {
        private readonly DataContext _context;

        public GirisController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sorgula(string tcKimlik)
        {

            if (string.IsNullOrEmpty(tcKimlik) || tcKimlik.Length != 11 || !tcKimlik.All(char.IsDigit))
            {
                ViewBag.HataMesaji = "TC Kimlik numarası 11 haneli olmalıdır ve sadece rakamlardan oluşmalıdır.";
                return View("Index");
            }

            var hasta = await _context.Hastalar.FirstOrDefaultAsync(h => h.TcKimlik == tcKimlik);

            if (hasta != null)
            {
                var randevular = _context.Randevular.Where(r => r.HastaId == hasta.HastaId)
                    .Include(r => r.Doktor)
                    .ThenInclude(d => d.Bolum);

                var viewModel = new RandevuViewModel
                {
                    HastaRandevulari = await randevular.ToListAsync(),
                    YeniRandevu = new Randevu { HastaId = hasta.HastaId, Hasta = hasta },
                    Hasta = hasta
                };

                ViewBag.Doktorlar = new SelectList(await _context.Doktorlar.ToListAsync(), "DoktorId", "DoktorAdSoyad");
                ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik");

                return View("Page2", viewModel);
            }

            return RedirectToAction("HastaOlustur", new { tcKimlik = tcKimlik });
        }



        public IActionResult HastaOlustur(string tcKimlik)
        {
            ViewBag.TcKimlik = tcKimlik; 
            ViewBag.Randevular = new List<Randevu>(); 
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HastaOlustur(Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                _context.Hastalar.Add(hasta);
                await _context.SaveChangesAsync();
                if (hasta.TcKimlik != null)
                {
                    return await Sorgula(hasta.TcKimlik);
                }
            }

            return View(hasta);
        }
    }
}