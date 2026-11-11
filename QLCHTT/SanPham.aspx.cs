using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class SanPham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT sp.MaSP AS [Mã sản phẩm], sp.TenSP AS [Tên sản phẩm], l.TenLoai AS [Danh mục],
       FORMAT(sp.DonGia, 'N0', 'vi-VN') + N' đ' AS [Đơn giá],
       sp.SoLuong AS [Số lượng], sp.KichThuoc AS [Kích thước], sp.MauSac AS [Màu sắc], sp.TrangThai AS [Trạng thái]
FROM dbo.SanPham sp INNER JOIN dbo.LoaiSanPham l ON sp.MaLoai = l.MaLoai
ORDER BY sp.MaSP");
            }
        }
    }
}
