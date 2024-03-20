using Microsoft.AspNetCore.Mvc;
using StoreWeb.Data;
using StoreWeb.ModelsView;

namespace StoreWeb.Controllers
{
    public class HangHoaController : Controller
    {
        private const int ElementCountPerPage = 6;
        private readonly Hshop2023Context _context;
        public HangHoaController(Hshop2023Context context) {
            _context = context;
        }
        public IActionResult Index(int? idloai,string? search,int page = 1)
        {
            
            var hanghoas = _context.HangHoas.Select(h => h);
            if(idloai != null )
            {
                hanghoas = hanghoas.Where(h => h.MaLoaiNavigation.MaLoai == idloai);
            }
            if(!String.IsNullOrEmpty(search) )
            {
                ViewData["SearchStr"] = search;
                hanghoas = hanghoas.Where(h => h.TenHh.Contains(search));
            }
            
            var hangHoaView = hanghoas.Select(h => new HangHoaViewModel() { MaHh = h.MaHh,TenHh= h.TenHh,
                DonGia = h.DonGia ?? 0,MoTaDonVi = h.MoTaDonVi ?? "",TenLoai = h.MaLoaiNavigation.TenLoai,HinhAnh = h.Hinh ?? ""}).AsEnumerable();
            ViewData["page"] = page;
            ViewData["countPage"] = (int)Math.Ceiling((float)hangHoaView.Count() / ElementCountPerPage);
            ViewData["IdLoai"] = idloai;
            hangHoaView = hangHoaView.Skip((page-1)* ElementCountPerPage).Take(ElementCountPerPage);
            return View(hangHoaView);
        }

        
    }
}
