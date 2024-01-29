using AreaOrenk.Areas.Admin.Models;
using AreaOrenk.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AreaOrenk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController(UygulamaDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        // GET: HomeController
        public async Task<ActionResult> IndexAsync()
        {
            var slaytListesi = await _db.Slaytlar.ToListAsync();
            var ayniSiraNumarasinaSahipler = slaytListesi.GroupBy(s => s.Sira)
                                       .Where(group => group.Count() > 1)
                                       .Select(group => group.Key)
                                       .ToList();

            ViewBag.DuplicateSiraNumbers = ayniSiraNumarasinaSahipler;
            return View(slaytListesi);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            var slayt = _db.Slaytlar.Find(id);
            if (slayt == null)
            {
                return NotFound();
            }
            return View(slayt);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(YeniSlaytViewModel yeniSlaytViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string dosyaAdi = Path.GetExtension(yeniSlaytViewModel.ResimDosyasi.FileName);
                    string yeniDosyaAdi = Guid.NewGuid().ToString() + dosyaAdi;
                    string dosyaYolu = Path.Combine(_env.WebRootPath, "img", yeniDosyaAdi);

                    using (var fs = new FileStream(dosyaYolu, FileMode.CreateNew))
                    {
                        yeniSlaytViewModel.ResimDosyasi.CopyTo(fs);
                    }

                    Slayt yeniSlayt = new Slayt
                    {
                        ResimYolu = yeniDosyaAdi,
                        Baslik = yeniSlaytViewModel.Baslik,
                        Aciklama = yeniSlaytViewModel.Aciklama,
                    };

                    // Sıra numarası kontrolünü burda yaptığım için bu hatayı alıyorum.
                    if (_db.Slaytlar.Any(s => s.Sira == yeniSlaytViewModel.Sira))
                    {
                        ModelState.AddModelError("Sira", "Bu sıra numarası şu anda kullanılmaktadır.");
                        return View(yeniSlaytViewModel);
                    }

                    yeniSlayt.Sira = yeniSlaytViewModel.Sira;

                    await _db.Slaytlar.AddAsync(yeniSlayt);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { islem = "basarili" });
                }
                else
                {
                    return View(yeniSlaytViewModel);
                }
            }
            catch
            {
                return View(yeniSlaytViewModel);
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            var duzenlenecekSlayt = _db.Slaytlar.Find(id);
            if (duzenlenecekSlayt == null)
            {
                return NotFound();
            }

            var duzenleViewModel = new DuzenleSlaytViewModel()
            {
                Id = id,
                Baslik = duzenlenecekSlayt.Baslik,
                Aciklama = duzenlenecekSlayt.Aciklama,
                Sira = duzenlenecekSlayt.Sira,
                ResimYolu = duzenlenecekSlayt.ResimYolu
            };
            return View(duzenleViewModel);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, DuzenleSlaytViewModel duzenleViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var duzenlenecekSlayt = await _db.Slaytlar.FindAsync(id);

                    if (duzenlenecekSlayt == null)
                    {
                        return NotFound();
                    }

                    duzenlenecekSlayt.Baslik = duzenleViewModel.Baslik;
                    duzenlenecekSlayt.Aciklama = duzenleViewModel.Aciklama;
                    duzenlenecekSlayt.Sira = duzenleViewModel.Sira;
                 

                    // Yeni Yüklenen Bir Resim Varsa
                    if (duzenleViewModel.ResimDosyasi != null)
                    {
                        ResmiDosyadanKaldir(duzenlenecekSlayt);

                        string dosyaAdi = Path.GetExtension(duzenleViewModel.ResimDosyasi.FileName);
                        string yeniDosyaAdi = Guid.NewGuid().ToString() + dosyaAdi;
                        string dosyaYolu = Path.Combine(_env.WebRootPath, "img", yeniDosyaAdi);

                        using (var fs = new FileStream(dosyaYolu, FileMode.CreateNew))
                        {
                            await duzenleViewModel.ResimDosyasi.CopyToAsync(fs);
                        }

                        duzenlenecekSlayt.ResimYolu = yeniDosyaAdi;
                    }

                    await _db.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), new { islem = "basarili"});
            }
            catch
            {
                return View(duzenleViewModel);
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            var silinecekSlayt = _db.Slaytlar.Find(id);
            if (silinecekSlayt == null)
            {
                return NotFound();
            }

            return View(silinecekSlayt);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slayt silinecekSlayt)
        {
            try
            {
                silinecekSlayt = await _db.Slaytlar.FindAsync(id);
                if (silinecekSlayt == null)
                {
                    return NotFound();
                }

                if (silinecekSlayt.ResimYolu != null)
                {
                    ResmiDosyadanKaldir(silinecekSlayt);
                }

                _db.Slaytlar.Remove(silinecekSlayt);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { islem = "basarili" });
            }
            catch
            {
                return View();
            }
        }

        public void ResmiDosyadanKaldir(Slayt slayt)
        {
            if (slayt.ResimYolu != null)
            {
                string silinecekDosyaAdi = slayt.ResimYolu;
                string silinecekDosyaYolu = Path.Combine(_env.WebRootPath, "img", silinecekDosyaAdi);

                if (System.IO.File.Exists(silinecekDosyaYolu))
                {
                    bool baskaSlaytTarafindanKullaniliyorMu = _db.Slaytlar.Any(s => s.ResimYolu == slayt.ResimYolu && s.Id != slayt.Id);
                    if (!baskaSlaytTarafindanKullaniliyorMu)
                    {
                        System.IO.File.Delete(silinecekDosyaYolu);
                    }
                }
            }
        }
    }
}
