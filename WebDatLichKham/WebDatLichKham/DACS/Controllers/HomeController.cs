using DACS.Data;
using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebDatLichKham.Models;
using WebDatLichKham.Repositories;

namespace DACS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBacsiRepository _bacsiRepository;
        private readonly Itinyte _tinYte;
        private readonly DatlichViewModel _datlichViewModel;
        private readonly IChuyenKhoaRepository _chuyenKhoaRepository;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, IBacsiRepository bacsiRepository, 
            Itinyte itinyte , DatlichViewModel datlichViewModel ,IChuyenKhoaRepository chuyenKhoaRepository,ApplicationDbContext context)
        {
            _logger = logger;
            _bacsiRepository = bacsiRepository;
            _tinYte = itinyte;
            _datlichViewModel = datlichViewModel;
            _chuyenKhoaRepository = chuyenKhoaRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tinyte = await _tinYte.GetAllAsync();
            return View(tinyte);
        }
        public async Task<IActionResult> Datlich()
        {
            var bacsis = await _bacsiRepository.GetAllAsync();
            var tintuc = await _tinYte.GetAllAsync();
            var chuyenkhoa= await _chuyenKhoaRepository.GetAllAsync();
            var viewModel = new DatlichViewModel
            {
                BacSis = bacsis,
                TinYtes = tintuc,
                Khoas = chuyenkhoa
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> tintuc(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _tinYte.GetByIdAsync(id);
              
               
            if (tinTuc == null)
            {
                return NotFound();
            }
            return View(tinTuc);    
                    

        }
        public async Task<IActionResult> Chuyenkhoa()
        {
            var bacsi = await _bacsiRepository.GetAllAsync();
            var khoa = await _chuyenKhoaRepository.GetAllAsync();
            var model = new DatlichViewModel
            {
                Khoas = khoa,
                BacSis = bacsi
            };
            return View(model);
        }
    }
}
