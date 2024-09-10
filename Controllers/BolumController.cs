using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers
{
	public class BolumController : Controller
	{
		private readonly DataContext _context;
        public BolumController(DataContext context)
        {
			_context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bolumler = await _context.Bolumler.ToListAsync();
            return View(bolumler);
        }
        public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BolumViewModel model)
        {
			if (ModelState.IsValid)
			{
				_context.Bolumler.Add(new Bolum() { BolumId = model.BolumId, Baslik = model.Baslik });
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

			var blm = await _context
							.Bolumler.Select(k => new BolumViewModel
							{
								BolumId = k.BolumId, //k bizim için daha önceden seçmiş oldugumuz veriye yani bölüme karşılık gelir seçilen bölümün idsini yeni oluşmuş view modele aktar ve daha sonra onu sayfa üzerine gönder demek.
								Baslik = k.Baslik
							})
							.FirstOrDefaultAsync(k => k.BolumId == id);
			if (blm == null)
			{
				return NotFound();
			}
			return View(blm);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, BolumViewModel model)
		{
			if (id != model.BolumId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(new Bolum() {BolumId = model.BolumId, Baslik = model.Baslik});
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateException)
				{
					if (!_context.Bolumler.Any(h => h.BolumId == model.BolumId))
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

			var bolum = await _context.Bolumler.FindAsync(id);

			if (bolum == null)
			{
				return NotFound();
			}
			return View(bolum);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete([FromForm] int id)
		{
			var bolum = await _context.Bolumler.FindAsync(id);
			if (bolum == null)
			{
				return NotFound();
			}
			_context.Bolumler.Remove(bolum);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}
