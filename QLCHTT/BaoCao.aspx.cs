using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class BaoCao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGiaTri(this, "lblDoanhThu", "SELECT FORMAT(ISNULL(SUM(TongTien),0), 'N0', 'vi-VN') + N' đ' FROM dbo.HoaDon WHERE TrangThai = N'Đã thanh toán'");
                DuLieu.GanGiaTri(this, "lblSoHoaDon", "SELECT COUNT(*) FROM dbo.HoaDon");
                DuLieu.GanGiaTri(this, "lblSanPhamDaBan", "SELECT ISNULL(SUM(SoLuong),0) FROM dbo.ChiTietHoaDon");
                DuLieu.GanGiaTri(this, "lblTonKho", "SELECT ISNULL(SUM(SoLuong),0) FROM dbo.SanPham");
                DuLieu.GanGrid(this, "gvData", @"SELECT TOP 10 sp.MaSP AS [Mã sản phẩm], sp.TenSP AS [Tên sản phẩm],
       SUM(ct.SoLuong) AS [Số lượng đã bán],
       FORMAT(SUM(ct.ThanhTien), 'N0', 'vi-VN') + N' đ' AS [Doanh thu]
FROM dbo.ChiTietHoaDon ct INNER JOIN dbo.SanPham sp ON ct.MaSP = sp.MaSP
GROUP BY sp.MaSP, sp.TenSP ORDER BY SUM(ct.SoLuong) DESC");
            }
        }
    }
}
