using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using WebDatLichKham.Repositories;
using WebDatLichKham.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DACS.Controllers
{
    public class BacSisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ZaloService _zaloService;
        private readonly ILogger<BacSisController> _logger;
        public BacSisController(ApplicationDbContext context, IEmailService emailService, UserManager<ApplicationUser> userManager, ZaloService zaloService, ILogger<BacSisController> logger)
        {
            _context = context;
            _emailService = emailService;
            _userManager = userManager;
            _zaloService = zaloService;
            _logger = logger;
        }

        // GET: BacSis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BacSis.Include(b => b.ChucVu).Include(b => b.Khoa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BacSis/Details/5
        public async Task<IActionResult> Details(int? id)
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
        //GET: hiển thị thông tin bác sĩ chọn ngày và hiển thị ca làm việc của bác sĩ
        [HttpGet]
        public async Task<IActionResult> GetWorkSchedules(int doctorId, DateTime selectedDate)
        {
            var workSchedules = await _context.LichLamViecs
                .Where(w => w.BacSiId == doctorId && w.Lich == selectedDate.Date)
                .ToListAsync();
         
            if (workSchedules == null || !workSchedules.Any())
            {

                ViewBag.ErrorMessage = "Không tìm thấy lịch làm việc.";
                return PartialView("_WorkSchedules", Enumerable.Empty<LichLamViec>());
            }

            return PartialView("_WorkSchedules", workSchedules);
        }
        [HttpPost]
        public async Task<IActionResult> SetTime(int lichId, string time)
        {
            // Tìm lịch làm việc theo Id
            var lichLamViec = await _context.LichLamViecs.FindAsync(lichId);

            if (lichLamViec == null)
            {
                return NotFound();
            }

            // Xử lý thời gian
            var ca = TimeOnly.Parse(time);

            //Kiểm tra xem có cuộc hẹn nào vào thời gian đã chọn không
            var datLichKham = await _context.DatLichKhams
                .FirstOrDefaultAsync(d => d.LichLamViecId == lichId && d.LichLamViec.Ca == ca);

            if (datLichKham != null)
            {
                return BadRequest("Thời gian đã được chọn không khả dụng.");
            }
            else
            {
                // Trả về thông tin lịch làm việc đã chọn cùng với danh sách ca làm việc
                var caLamViecs = await _context.LichLamViecs.Where(l => l.Id == lichId).ToListAsync();
                return Ok(caLamViecs);
            }
        }

        // GET: BacSis/Create
        public IActionResult Create()
        {
            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "Id");
            ViewData["KhoaId"] = new SelectList(_context.Khoas, "Id", "Id");
            return View();
        }

        // POST: BacSis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GioiTinh,ChucVuId,KhoaId,NgaySinh,DiaChi,SoDienThoai,GioiThieu,KinhNghiem,HocVan,ImageUrl")] BacSi bacSi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bacSi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "Id", bacSi.ChucVuId);
            ViewData["KhoaId"] = new SelectList(_context.Khoas, "Id", "Id", bacSi.KhoaId);
            return View(bacSi);
        }

        // GET: BacSis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSi = await _context.BacSis.FindAsync(id);
            if (bacSi == null)
            {
                return NotFound();
            }
            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "Id", bacSi.ChucVuId);
            ViewData["KhoaId"] = new SelectList(_context.Khoas, "Id", "Id", bacSi.KhoaId);
            return View(bacSi);
        }

        // POST: BacSis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GioiTinh,ChucVuId,KhoaId,NgaySinh,DiaChi,SoDienThoai,GioiThieu,KinhNghiem,HocVan,ImageUrl")] BacSi bacSi)
        {
            if (id != bacSi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bacSi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacSiExists(bacSi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChucVuId"] = new SelectList(_context.ChucVus, "Id", "Id", bacSi.ChucVuId);
            ViewData["KhoaId"] = new SelectList(_context.Khoas, "Id", "Id", bacSi.KhoaId);
            return View(bacSi);
        }

        // GET: BacSis/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: BacSis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);
            if (bacSi != null)
            {
                _context.BacSis.Remove(bacSi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BacSiExists(int id)
        {
            return _context.BacSis.Any(e => e.Id == id);
        }


        [HttpGet]
        public IActionResult NhapThongTinBenhNhan(int? lichId)
        {
            if (lichId == null)
            {
                return NotFound();
            }

            // Lấy thông tin lịch làm việc để truyền vào view
            var lichLamViec = _context.LichLamViecs
                                    .Include(l => l.BacSi) // Bao gồm thông tin bác sĩ
                                    .FirstOrDefault(l => l.Id == lichId);
            if (lichLamViec == null)
            {
                return NotFound();
            }

            ViewBag.LichId = lichId;
            ViewBag.BacSi = lichLamViec.BacSi;
            ViewBag.LichLamViec = lichLamViec;
            ViewBag.Ca = lichLamViec.Ca;
            ViewBag.Lich = lichLamViec.Lich;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NhapThongTinBenhNhan(string Name, string GioiTinh, DateTime NgaySinh, string DiaChi, string SoDienThoai, List<IFormFile> imageUrls, int lichId, string Gmail, string TinhTrang)
        {
            if (ModelState.IsValid)
            {
                var benhNhan = new BenhNhan
                {
                    Name = Name,
                    GioiTinh = GioiTinh,
                    NgaySinh = NgaySinh,
                    DiaChi = DiaChi,
                    SoDienThoai = SoDienThoai,
                    TinhTrang = TinhTrang,
                    Gmail = Gmail,
                };

                foreach (var image in imageUrls)
                {
                    var imagePath = await SaveImage(image);
                    benhNhan.Images.Add(new Image { ImageUrl = imagePath, Name = image.FileName });
                }

                _context.BenhNhans.Add(benhNhan);
                await _context.SaveChangesAsync();

                var datLichKham = new DatLichKham
                {
                    BenhNhanId = benhNhan.Id,
                    LichLamViecId = lichId,
                    NgayDatLich = DateTime.Now,
                    TrangThai = "Đang chờ xác nhận"
                };

                _context.DatLichKhams.Add(datLichKham);
                await _context.SaveChangesAsync();

                var lichLamViec = _context.LichLamViecs.FirstOrDefault(l => l.Id == lichId);
                var chiTietDatLichKham = new ChiTietDatLichKham
                {
                    DatLichKhamId = datLichKham.Id,
                    BacSiId = lichLamViec.BacSiId,
                    GhiChu = "Đang chờ"
                };

                _context.ChiTietDatLichKhams.Add(chiTietDatLichKham);
                await _context.SaveChangesAsync();

                var eMailInfo = new EmailSender
                {
                    name = "Bệnh Viện Nguyễn Trãi",
                    To = benhNhan.Gmail,
                    subject = "Thông báo lịch hẹn",
                    conTent = "<p style='color:red;'>Thông tin Lịch Khám: Đang chờ duyệt </p>" +
                              "<p><strong>Họ tên:</strong> " + Name + "</p>" +
                              "<p><strong>Giới tính:</strong> " + GioiTinh + "</p>" +
                              "<p><strong>Ngày đặt lịch :</strong> " + lichLamViec.Lich.ToString("dd/MM/yyyy") + "</p>" +
                              "<p><strong>Số điện thoại:</strong> " + SoDienThoai + "</p>",
                };
                await _emailService.send(eMailInfo);

                var zaloMessage = $"Thông tin Lịch Khám: Đang chờ duyệt\nHọ tên: {Name}\nGiới tính: {GioiTinh}\nNgày đặt lịch: {lichLamViec.Lich:dd/MM/yyyy}\nSố điện thoại: {SoDienThoai}";
                var zaloResponse = await _zaloService.SendMessageAsync(SoDienThoai, zaloMessage);

                if (zaloResponse == "success")
                {
                    TempData["SmsSuccess"] = "Tin nhắn Zalo đã được gửi thành công.";
                }
                else
                {
                    TempData["SmsError"] = $"Lỗi khi gửi tin nhắn Zalo: {zaloResponse}";
                    // Log lỗi để xem chi tiết
                    _logger.LogError($"Lỗi khi gửi tin nhắn Zalo: {zaloResponse}");
                }

                return RedirectToAction("DatLich", "Home");
            }

            return View();
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

    }
}
