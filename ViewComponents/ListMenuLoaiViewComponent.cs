using Microsoft.AspNetCore.Mvc;
using StoreWeb.Data;
using StoreWeb.ModelsView;

namespace StoreWeb.ViewComponents
{
    public class ListMenuLoaiViewComponent: ViewComponent
    {
        private Hshop2023Context _context { get; set; }
        public ListMenuLoaiViewComponent(Hshop2023Context context) { 
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categoryCustom = _context.Loais.Select(l => new LoaiCustomModel
            {
                MaLoai = l.MaLoai, TenLoai = l.TenLoai, SoLuongHang = l.HangHoas.Count
            });
            return View("Default",categoryCustom);
        }
    }
}
