using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers
{
    public class RandevuController : Controller
    {
        private readonly DataContext _context;
        public RandevuController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, string timeFilter)
        {
            var randevular = _context
                .Randevular
                .Include(x => x.Hasta)
                .Include(x => x.Doktor)
                .ThenInclude(d => d.Bolum)
                .AsQueryable();

            // Hasta adı ile arama
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                randevular = randevular.Where(r => r.Hasta.HastaAd.ToLower().Contains(searchString));
                ViewBag.SearchString = searchString;
            }

            // Zaman filtresi
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(timeFilter) && timeFilter != "all")
            {
                int months = int.Parse(timeFilter);
                DateTime dateFrom = now.AddMonths(-months);
                randevular = randevular.Where(r => r.KayitTarihi >= dateFrom);
            }

            // Seçili filtreyi sakla
            ViewBag.SelectedTimeFilter = timeFilter;

            return View(await randevular.ToListAsync());
        }







        public async Task<IActionResult> Create()
        {
            ViewBag.Hastalar = new SelectList(await _context.Hastalar.ToListAsync(), "HastaId", "HastaAd");
            ViewBag.Doktorlar = new SelectList(await _context.Doktorlar.ToListAsync(), "DoktorId", "DoktorAdSoyad");
            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RandevuViewModel model)
        {
            _context.Randevular.Add(model.YeniRandevu);
            await _context.SaveChangesAsync();

            var hasta = await _context.Hastalar.FirstOrDefaultAsync(h => h.HastaId == model.YeniRandevu.HastaId);

            if (hasta != null && hasta.TcKimlik != null)
            {
                return await new GirisController(_context).Sorgula(hasta.TcKimlik);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context
                               .Randevular
                               .FirstOrDefaultAsync(o => o.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }
            randevu.Doktor = await _context.Doktorlar.FirstOrDefaultAsync(d => d.DoktorId == randevu.DoktorId);

            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik", randevu.Doktor.BolumId);

            ViewBag.Hastalar = new SelectList(await _context.Hastalar.ToListAsync(), "HastaId", "HastaAd");
            ViewBag.Doktorlar = new SelectList(await _context.Doktorlar.ToListAsync(), "DoktorId", "DoktorAdSoyad");
            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Randevu model)
        {
            if (id != model.RandevuId)
            {
                return NotFound();
            }

            model.Hasta = await _context.Hastalar.FirstOrDefaultAsync(h => h.HastaId == model.HastaId);
            model.Doktor = await _context.Doktorlar.FirstOrDefaultAsync(d => d.DoktorId == model.DoktorId);
            ModelState.Remove("Hasta");
            ModelState.Remove("Doktor");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Randevular.Any(h => h.RandevuId == model.RandevuId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}