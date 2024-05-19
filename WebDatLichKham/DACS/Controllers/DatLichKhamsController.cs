using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.Repositories;
using WebDatLichKham.Models;
using WebDatLichKham.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DACS.Controllers
{
    public class DatLichKhamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBenhNhanRepository _benhNhanRepository;
        private readonly ILichLamViecRepository _ilichLamViecRepository;
        private readonly ILichKhamBenhRepository _ilichKhamBenhRepository;

        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public DatLichKhamsController(ApplicationDbContext context, ILichLamViecRepository ilichLamViecRepository,
            ILichKhamBenhRepository ilichKhamBenhRepository, IBenhNhanRepository benhNhanRepository, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _ilichLamViecRepository = ilichLamViecRepository;
            _ilichKhamBenhRepository = ilichKhamBenhRepository;
            _benhNhanRepository = benhNhanRepository;
            _emailService = emailService;
            _userManager = userManager;
        }

        // GET: DatLichKhams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DatLichKhams.Include(d => d.BenhNhan).Include(d => d.LichLamViec);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> IndexWaitForConfirm()
        {
            var appointments = await _context.DatLichKhams
                                   .Include(dl => dl.LichLamViec)
                                   .Include(dl => dl.BenhNhan)
                                   .Where(a => a.TrangThai == "Đang chờ xác nhận")
                                   .ToListAsync();
            return View(appointments);
        }
        // POST: Appointments/Confirm/5
        [HttpPost]
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid appointment id.");
            }
            var appointment = await _context.DatLichKhams.FindAsync(id.Value);
            var benhnhan = await _benhNhanRepository.GetByIdAsync(appointment.BenhNhanId);
            var lichlamviec = await _ilichLamViecRepository.GetByIdAsync(appointment.LichLamViecId);
            if (appointment.BenhNhanId != null)
            {
                var eMailInfo = new EmailSender
                {
                    name = "Bệnh Viện Nguyễn Trãi",
                    To = benhnhan.Gmail,
                    subject = "Thông báo lịch hẹn đã được chấp nhận",
                    conTent = "<div style='color:#333333; font-family: Arial, sans-serif;'>" +
                               "<table style='width:100%;'>" +
                               "<tr>" +
                               "<td style='width:30%;'><strong>Họ tên:</strong></td>" +
                               "<td>" + benhnhan.Name + "</td>" +
                               "</tr>" +
                               "<tr>" +
                               "<td><strong>Giới tính:</strong></td>" +
                               "<td>" + benhnhan.GioiTinh + "</td>" +
                               "</tr>" +
                               "<tr>" +
                               "<td><strong>Giờ :</strong></td>" +
                               "<td>" + lichlamviec.Ca.ToString("HH:mm") + "</td>" +
                               "</tr>" +
                               "<tr>" +
                               "<td><strong>Ngày đặt lịch :</strong></td>" +
                               "<td>" + lichlamviec.Lich.ToString("dd/MM/yyyy") + "</td>" +
                               "</tr>" +
                               "<tr>" +
                               "<td><strong>Địa chỉ:</strong></td>" +
                               "<td>" + benhnhan.DiaChi + "</td>" +
                               "</tr>" +
                               "<tr>" +
                               "<td><strong>Số điện thoại:</strong></td>" +
                               "<td>" + benhnhan.SoDienThoai + "</td>" +
                               "</tr>" +
                               "</table>" +
                               "</div>"
                };
                await _emailService.send(eMailInfo);
            }
            appointment.TrangThai = "Đã xác nhận";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private static int? GetBenhNhanId(DatLichKham appointment)
        {
            return appointment.BenhNhanId;
        }

        // POST: Appointments/Cancel/5
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var appointment = await _context.DatLichKhams.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.TrangThai = "Đã hủy";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> IndexConfirm()
        {
            var appointments = await _context.DatLichKhams
                                   .Include(dl => dl.LichLamViec)
                                   .Include(dl => dl.BenhNhan)
                                   .Where(a => a.TrangThai == "Đã xác nhận")
                                   .ToListAsync();
            return View(appointments);
        }
        [HttpPost]
        public async Task<IActionResult> Examin(int id)
        {
            var appointment = await _context.DatLichKhams.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.TrangThai = "Đã khám";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> IndexExamin()
        {
            var appointments = await _context.DatLichKhams
                                   .Include(dl => dl.LichLamViec)
                                   .Include(dl => dl.BenhNhan)
                                   .Where(a => a.TrangThai == "Đã khám")
                                   .ToListAsync();
            return View(appointments);
        }
        public async Task<IActionResult> IndexCancelled()
        {
            var appointments = await _context.DatLichKhams
                                   .Include(dl => dl.LichLamViec)
                                   .Include(dl => dl.BenhNhan)
                                   .Where(a => a.TrangThai == "Đã hủy")
                                   .ToListAsync();
            return View(appointments);
        }
        // GET: DatLichKhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datLichKham = await _context.DatLichKhams
                .Include(d => d.BenhNhan)
                .Include(d => d.LichLamViec)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datLichKham == null)
            {
                return NotFound();
            }

            return View(datLichKham);
        }

        // GET: DatLichKhams/Create
        public IActionResult Create()
        {
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhans, "Id", "Id");
            ViewData["LichLamViecId"] = new SelectList(_context.LichLamViecs, "Id", "Id");
            return View();
        }

        // POST: DatLichKhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BenhNhanId,LichLamViecId,NgayDatLich,TrangThai")] DatLichKham datLichKham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datLichKham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhans, "Id", "Id", datLichKham.BenhNhanId);
            ViewData["LichLamViecId"] = new SelectList(_context.LichLamViecs, "Id", "Id", datLichKham.LichLamViecId);
            return View(datLichKham);
        }

        // GET: DatLichKhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datLichKham = await _context.DatLichKhams.FindAsync(id);
            if (datLichKham == null)
            {
                return NotFound();
            }
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhans, "Id", "Id", datLichKham.BenhNhanId);
            ViewData["LichLamViecId"] = new SelectList(_context.LichLamViecs, "Id", "Id", datLichKham.LichLamViecId);
            return View(datLichKham);
        }

        // POST: DatLichKhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BenhNhanId,LichLamViecId,NgayDatLich,TrangThai")] DatLichKham datLichKham)
        {
            if (id != datLichKham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datLichKham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatLichKhamExists(datLichKham.Id))
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
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhans, "Id", "Id", datLichKham.BenhNhanId);
            ViewData["LichLamViecId"] = new SelectList(_context.LichLamViecs, "Id", "Id", datLichKham.LichLamViecId);
            return View(datLichKham);
        }

        // GET: DatLichKhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datLichKham = await _context.DatLichKhams
                .Include(d => d.BenhNhan)
                .Include(d => d.LichLamViec)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datLichKham == null)
            {
                return NotFound();
            }

            return View(datLichKham);
        }

        // POST: DatLichKhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datLichKham = await _context.DatLichKhams.FindAsync(id);
            if (datLichKham != null)
            {
                _context.DatLichKhams.Remove(datLichKham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatLichKhamExists(int id)
        {
            return _context.DatLichKhams.Any(e => e.Id == id);
        }
    }
}
