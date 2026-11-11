using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {
                DuLieu.GanGiaTri(this, "lblTongSanPham", "SELECT COUNT(*) FROM dbo.SanPham");
                DuLieu.GanGiaTri(this, "lblSoHoaDon", "SELECT COUNT(*) FROM dbo.HoaDon");
                DuLieu.GanGiaTri(this, "lblSoKhachHang", "SELECT COUNT(*) FROM dbo.KhachHang");
                DuLieu.GanGiaTri(this, "lblDoanhThu", "SELECT FORMAT(ISNULL(SUM(TongTien),0), 'N0', 'vi-VN') + N' đ' FROM dbo.HoaDon WHERE TrangThai = N'Đã thanh toán'");
                DuLieu.GanGrid(this, "gvData", @"SELECT hd.MaHD AS [Mã hóa đơn], CONVERT(varchar(10), hd.NgayLap, 103) AS [Ngày lập],
       kh.HoTen AS [Khách hàng], nv.HoTen AS [Nhân viên],
       FORMAT(hd.TongTien, 'N0', 'vi-VN') + N' đ' AS [Tổng tiền],
       hd.TrangThai AS [Trạng thái], ISNULL(km.TenKM, N'Không áp dụng') AS [Khuyến mãi]
FROM dbo.HoaDon hd
INNER JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
INNER JOIN dbo.NhanVien nv ON hd.MaNV = nv.MaNV
LEFT JOIN dbo.KhuyenMai km ON hd.MaKM = km.MaKM
ORDER BY hd.NgayLap DESC, hd.MaHD");
                DuLieu.GanGrid(this, "gvTonKho", @"SELECT TOP 5 TenSP AS [Sản phẩm], SoLuong AS [Số lượng tồn], TrangThai AS [Trạng thái]
FROM dbo.SanPham ORDER BY SoLuong ASC");
            }
        }
    }
}
