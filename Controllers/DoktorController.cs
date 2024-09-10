using HospitalApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers
{
    public class DoktorController : Controller
    {
        private readonly DataContext _context;

        public DoktorController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Doktorlar.Include(d => d.Bolum).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doktor model)
        {
            var bolum = await _context.Bolumler.FindAsync(model.BolumId);
            if (bolum == null)
            {
                ModelState.AddModelError("BolumId", "Lütfen geçerli bir bölüm seçiniz.");
            }

            if (ModelState.IsValid)
            {
                model.Bolum = bolum;
                _context.Doktorlar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }

            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik", doktor.BolumId);
            return View(doktor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doktor model)
        {
            if (id != model.DoktorId)
            {
                return NotFound();
            }

            var bolum = await _context.Bolumler.FindAsync(model.BolumId);
            if (bolum == null)
            {
                ModelState.AddModelError("BolumId", "Lütfen geçerli bir bölüm seçiniz.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    model.Bolum = bolum;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Doktorlar.Any(e => e.DoktorId == model.DoktorId))
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

            ViewBag.Bolumler = new SelectList(await _context.Bolumler.ToListAsync(), "BolumId", "Baslik", model.BolumId);
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Bolum)
                .FirstOrDefaultAsync(m => m.DoktorId == id);

            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doktor = await _context.Doktorlar.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }

            _context.Doktorlar.Remove(doktor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> GetDoktorlar(int bolumId)
        {
            var doktorlar = await _context.Doktorlar
                .Where(d => d.BolumId == bolumId)
                .Select(d => new { d.DoktorId, d.DoktorAdSoyad })
                .ToListAsync();

            return Json(doktorlar);
        }
    }
}
