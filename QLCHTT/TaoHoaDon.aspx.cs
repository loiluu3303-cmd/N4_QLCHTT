using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class TaoHoaDon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DuLieu.GanGrid(this, "gvData", @"SELECT MaSP AS [Mã sản phẩm], TenSP AS [Tên sản phẩm], DonGia AS [Đơn giá],
       SoLuong AS [Số lượng tồn], KichThuoc AS [Kích thước], MauSac AS [Màu sắc], TrangThai AS [Trạng thái]
FROM dbo.SanPham ORDER BY TenSP");
        }
    }
}
