using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HospitalApp.Controllers
{
    public class HastaController : Controller
    {
        private readonly DataContext _context;

        public HastaController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hastalar.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hasta model)
        {
            if (ModelState.IsValid) 
            {
                _context.Hastalar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hst = await _context
                            .Hastalar
                            .FirstOrDefaultAsync(o => o.HastaId == id);
            if (hst == null)
            {
                return NotFound();
            }

            return View(hst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hasta model)
        {
            if (id != model.HastaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Hastalar.Any(h => h.HastaId == model.HastaId))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForGiris(int id, RandevuViewModel model)
        {
            if (id != model.Hasta.HastaId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(model.Hasta);
                await _context.SaveChangesAsync();
                var hasta = await _context.Hastalar.FirstOrDefaultAsync(h => h.HastaId == model.Hasta.HastaId);

                return hasta != null && hasta.TcKimlik != null ? await new GirisController(_context).Sorgula(hasta.TcKimlik) : NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Hastalar.Any(h => h.HastaId == model.Hasta.HastaId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hastalar.FindAsync(id);

            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var hasta = await _context.Hastalar.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }

            _context.Hastalar.Remove(hasta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}