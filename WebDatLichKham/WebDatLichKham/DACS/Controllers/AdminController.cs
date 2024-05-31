using DACS.Repositories;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using WebDatLichKham.Repositories;
using WebDatLichKham.Extensions.WebDatLichKham.Extensions;
namespace DACS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IBacsiRepository _bacsiRepository;
        private readonly ILichLamViecRepository _lichLamViecRepository;
        private readonly IChuyenKhoaRepository _chuyenKhoaRepository;
        private readonly IChucVurepository _chucVuRepository;
        private readonly Itinyte _tinYte;
        private readonly ApplicationDbContext _context;
        private readonly ILichKhamBenhRepository _lichkhambenhRepository;
        public AdminController(IBacsiRepository bacsiRepository, ILichLamViecRepository lichLamViecRepository,
            IChuyenKhoaRepository chuyenKhoaRepository, IChucVurepository chucVuRepository,
            ApplicationDbContext context, Itinyte tinyte, ILichKhamBenhRepository lichKhamBenhRepository)
        {
            _bacsiRepository = bacsiRepository;
            _lichLamViecRepository = lichLamViecRepository;
            _chuyenKhoaRepository = chuyenKhoaRepository;
            _chucVuRepository = chucVuRepository;
            _context = context;
            _tinYte = tinyte;
            _lichkhambenhRepository=lichKhamBenhRepository;

        }
        /* public async Task<IActionResult> Index()
         {
             var applicationDbContext = _context.DatLichKhams.Include(d => d.BenhNhan).Include(d => d.LichLamViec);
             return View(await applicationDbContext.ToListAsync());
         }*/
        public async Task<IActionResult> Index()
        {
            var totalPatients = await _context.BenhNhans.CountAsync();
            var totalAppointments = await _context.DatLichKhams.CountAsync();
            var completedAppointments = await _context.DatLichKhams.Where(a => a.TrangThai == "Đã khám").CountAsync();
            var totalDoctors = await _context.BacSis.CountAsync();

            var model = new
            {
                TotalPatients = totalPatients,
                TotalAppointments = totalAppointments,
                CompletedAppointments = completedAppointments,
                TotalDoctors = totalDoctors
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientData(string timeframe)
        {
            var query = _context.DatLichKhams.AsQueryable();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;

            switch (timeframe)
            {
                case "week":
                    startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    endDate = startDate.AddDays(6);
                    var weekData = await query
                        .Where(a => a.NgayDatLich >= startDate && a.NgayDatLich <= endDate)
                        .GroupBy(a => new { a.NgayDatLich.Year, a.NgayDatLich.Month, a.NgayDatLich.Day })
                        .Select(g => new
                        {
                            label = g.Key.Day + "/" + g.Key.Month,
                            count = g.Count()
                        })
                        .ToListAsync();
                    return Json(weekData);

                case "month":
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    var monthData = await query
                        .Where(a => a.NgayDatLich >= startDate && a.NgayDatLich <= endDate)
                        .GroupBy(a => new { a.NgayDatLich.Year, a.NgayDatLich.Month, Week = EF.Functions.DateDiffDay(new DateTime(a.NgayDatLich.Year, a.NgayDatLich.Month, 1), a.NgayDatLich) / 7 })
                        .Select(g => new
                        {
                            label = "Tuần " + (g.Key.Week + 1),
                            count = g.Count()
                        })
                        .ToListAsync();
                    return Json(monthData);

                case "year":
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                    endDate = new DateTime(DateTime.Now.Year, 12, 31);
                    var yearData = await query
                        .Where(a => a.NgayDatLich >= startDate && a.NgayDatLich <= endDate)
                        .GroupBy(a => new { a.NgayDatLich.Year, a.NgayDatLich.Month })
                        .Select(g => new
                        {
                            label = "Tháng " + g.Key.Month,
                            count = g.Count()
                        })
                        .ToListAsync();
                    return Json(yearData);
            }

            return Json(new { });
        }
        public async Task<IActionResult> IndexDoctor()
        {
            var applicationDbContext = _context.BacSis.Include(b => b.ChucVu).Include(b => b.Khoa);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> AddBacsi()
        {
            var chuyenkhoas = await _chuyenKhoaRepository.GetAllAsync();
            var chucvus = await _chucVuRepository.GetAllAsync();
            ViewBag.chucvus = new SelectList(chucvus, "Id", "Name");
            ViewBag.chuyenkhoas = new SelectList(chuyenkhoas, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBacsi(BacSi bacSi, IFormFile imageUrl)
        {
            try
            {
                if (imageUrl != null)
                {
                    bacSi.ImageUrl = await SaveImage(imageUrl);
                }
                await _bacsiRepository.AddAsync(bacSi);
                return RedirectToAction("IndexDoctor");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm bác sĩ vào cơ sở dữ liệu: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); // 
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Trả về đường dẫn tương đối
        }
        public async Task<IActionResult> Addchucvu()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Addchucvu(ChucVu chucVu)
        {
            try
            {
                await _chucVuRepository.AddAsync(chucVu);
                return RedirectToAction("IndexChucVu");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm  Chức Vụ  vào cơ sở dữ liệu: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> AddChuyenKhoa()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddChuyenKhoa(Khoa khoa)
        {
            try
            {
                await _chuyenKhoaRepository.AddAsync(khoa);
                return RedirectToAction("IndexKhoa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm Khoa vào cơ sở dữ liệu: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        // GET: BenhNhans
        public async Task<IActionResult> IndexBenhNhan()
        {
            return View(await _context.BenhNhans
             .OrderByDescending(k => k.Id)
             .Include(b => b.Images)
             .ToListAsync());
        }
        // GET: BacSis/Details/5
        public async Task<IActionResult> DetailsBacSi(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSi = await _context.BacSis
                .Include(b => b.ChucVu)
                .Include(b => b.Khoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacSi == null)
            {
                return NotFound();
            }

            return View(bacSi);
        }
        // GET: BacSis/Delete/5
        public async Task<IActionResult> DeleteBacSi(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSi = await _context.BacSis
                .Include(b => b.ChucVu)
                .Include(b => b.Khoa)
                .Include(b=>b.LichLamViecs)
                .Include(b=>b.ChiTietLichKhamBenhs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacSi == null)
            {
                return NotFound();
            }

            return View(bacSi);
        }

        // POST: BacSis/Delete/5
        [HttpPost, ActionName("DeleteBacSi")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);
            if (bacSi != null)
            {
                _context.BacSis.Remove(bacSi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexDoctor));
        }

        private bool BacSiExists(int id)
        {
            return _context.BacSis.Any(e => e.Id == id);
        }
        public async Task<IActionResult> IndexChucVu()
        {
            return View(await _context.ChucVus.ToListAsync());
        }
        // GET: ChucVus/Delete/5
        public async Task<IActionResult> DeleteChucVu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // POST: ChucVus/Delete/5
        [HttpPost, ActionName("DeleteChucVu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedChucVu(int id)
        {
            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu != null)
            {
                _context.ChucVus.Remove(chucVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexChucVu));
        }

        private bool ChucVuExists(int id)
        {
            return _context.ChucVus.Any(e => e.Id == id);
        }// GET: Khoas
        public async Task<IActionResult> IndexKhoa()
        {
            var khoas = await _context.Khoas
                                     .OrderByDescending(k => k.Id)
                                     .ToListAsync();
            return View(khoas);
        }
        // GET: Khoas/Delete/5
        public async Task<IActionResult> DeleteKhoa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoa = await _context.Khoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoa == null)
            {
                return NotFound();
            }

            return View(khoa);
        }

        // POST: Khoas/Delete/5
        [HttpPost, ActionName("DeleteKhoa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedKhoa(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa != null)
            {
                _context.Khoas.Remove(khoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexKhoa));
        }

        private bool KhoaExists(int id)
        {
            return _context.Khoas.Any(e => e.Id == id);
        }
        public async Task<IActionResult> IndexLichLamViec()
        {
            var applicationDbContext = _context.LichLamViecs.Include(l => l.BacSi);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult AddLichLamViec()
        {
            ViewData["BacSiId"] = new SelectList(_context.BacSis, "Id", "Name");
            return View();
        }

        // POST: LichLamViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLichLamViec([Bind("Id,BacSiId,Lich,Ca")] LichLamViec lichLamViec)
        {
            
                    var selectedTimes = Request.Form["Ca"].ToString().Split(',');

                    foreach (var time in selectedTimes)
                    {
                        var caLamViec = new LichLamViec
                        {
                            BacSiId = lichLamViec.BacSiId,
                            Lich = lichLamViec.Lich,
                            Ca = TimeOnly.ParseExact(time, "HH:mm", null)
                        };

                        _context.Add(caLamViec);
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(IndexLichLamViec));
             
        }
        // GET: LichLamViecs/Delete/5
        public async Task<IActionResult> DeleteLichLamViec(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichLamViec = await _context.LichLamViecs
                .Include(l => l.BacSi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichLamViec == null)
            {
                return NotFound();
            }

            return View(lichLamViec);
        }

        // POST: LichLamViecs/Delete/5
        [HttpPost, ActionName("DeleteLichLamViec")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedLichLamViec(int id)
        {
            var lichLamViec = await _context.LichLamViecs.FindAsync(id);
            if (lichLamViec != null)
            {
                _context.LichLamViecs.Remove(lichLamViec);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexLichLamViec");
        }

        private bool LichLamViecExists(int id)
        {
            return _context.LichLamViecs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddTinYTe()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTinYTe(TinYTe tinYTe, IFormFile imageUrl)
        {
            try
            {
                if (imageUrl != null)
                {
                    tinYTe.ImageUrl = await SaveImage(imageUrl);
                }
                tinYTe.NgayDang = DateTime.Now;
                await _tinYte.AddAsync(tinYTe);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm bác sĩ vào cơ sở dữ liệu: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> IndexTinYte()
        {
            var applicationDbContext = _context.TinYTes;
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> DeleteTinYte(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinYte = await _context.TinYTes.FirstOrDefaultAsync(m => m.Id == id);
            if (tinYte == null)
            {
                return NotFound();
            }

            return View(tinYte);
        }

        // POST: LichLamViecs/Delete/5
        [HttpPost, ActionName("DeleteTinYte")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTinYte(int id)
        {
            var tinYte = await _context.TinYTes.FindAsync(id);
            if (tinYte != null)
            {
                _context.TinYTes.Remove(tinYte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBenhNhan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhans
                .Include(b => b.LichKhamBenhs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // POST: BacSis/Delete/5
        [HttpPost, ActionName("DeleteBenhNhan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedBenhNhan(int id)
        {
            var benhNhan = await _context.BenhNhans.FindAsync(id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            // Tìm và xóa các bản ghi liên quan trong bảng DatLichKhams
            var datLichKhams = await _context.DatLichKhams
                              .Include(d => d.ChiTietDatLichKhams)
                              .Where(d => d.BenhNhanId == id)
                              .ToListAsync();

            foreach (var datLichKham in datLichKhams)
            {
                var chiTietDatLichKhams = await _context.ChiTietDatLichKhams
                      .Where(c => c.DatLichKhamId == datLichKham.Id)
                        .ToListAsync();
                _context.ChiTietDatLichKhams.RemoveRange(chiTietDatLichKhams);

                // Xóa bản ghi từ bảng DatLichKhams
                _context.DatLichKhams.Remove(datLichKham);
            }

            // Xóa bệnh nhân
            _context.BenhNhans.Remove(benhNhan);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexBenhNhan));
        }

        public async Task<IActionResult> DeleteLichDatKham(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datLichKham = await _context.DatLichKhams
                .FirstOrDefaultAsync(m => m.Id == id);

            if (datLichKham == null)
            {
                return NotFound();
            }

            return View(datLichKham);
        }

        [HttpPost, ActionName("DeleteLichDatKham")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedDatLichKham(int id)
        {
            var datLichKham = await _context.DatLichKhams
                .Include(d => d.ChiTietDatLichKhams)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (datLichKham == null)
            {
                return NotFound();
            }

            _context.DatLichKhams.Remove(datLichKham);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> EditTinYTe(int id)
        {
            var tinYTe = await _tinYte.GetByIdAsync(id);
            if (tinYTe == null)
            {
                return NotFound();
            }
            return View(tinYTe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTinYTe(int id, TinYTe tinYTe, IFormFile imageUrl)
        {
            if (id != tinYTe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageUrl != null)
                    {
                        tinYTe.ImageUrl = await SaveImages(imageUrl);
                    }
                    await _tinYte.UpdateAsync(tinYTe);
                    return RedirectToAction(nameof(IndexTinYte));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TinYTeExists(tinYTe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(tinYTe);
        }

        private async Task<string> SaveImages(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        private async Task<bool> TinYTeExists(int id)
        {
            return await _tinYte.GetByIdAsync(id) != null;
        }



    }
}
