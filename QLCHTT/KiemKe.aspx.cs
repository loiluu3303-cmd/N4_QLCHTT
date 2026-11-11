using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KiemKe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT sp.MaSP AS [Mã sản phẩm], sp.TenSP AS [Tên sản phẩm],
       l.TenLoai AS [Danh mục], sp.SoLuong AS [Số lượng theo hệ thống],
       sp.MauSac AS [Màu sắc], sp.KichThuoc AS [Kích thước]
FROM dbo.SanPham sp INNER JOIN dbo.LoaiSanPham l ON sp.MaLoai = l.MaLoai
ORDER BY sp.MaSP");
            }
        }
    }
}
