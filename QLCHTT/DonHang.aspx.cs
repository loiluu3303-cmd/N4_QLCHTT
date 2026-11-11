using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DonHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT hd.MaHD AS [Mã hóa đơn], CONVERT(varchar(10), hd.NgayLap, 103) AS [Ngày lập],
       kh.HoTen AS [Khách hàng], nv.HoTen AS [Nhân viên],
       FORMAT(hd.TongTien, 'N0', 'vi-VN') + N' đ' AS [Tổng tiền],
       hd.TrangThai AS [Trạng thái], ISNULL(km.TenKM, N'Không áp dụng') AS [Khuyến mãi]
FROM dbo.HoaDon hd
INNER JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
INNER JOIN dbo.NhanVien nv ON hd.MaNV = nv.MaNV
LEFT JOIN dbo.KhuyenMai km ON hd.MaKM = km.MaKM
ORDER BY hd.NgayLap DESC, hd.MaHD");
                DuLieu.GanGrid(this, "gvChiTiet", @"SELECT ct.MaHD AS [Mã hóa đơn], ct.MaSP AS [Mã sản phẩm], sp.TenSP AS [Tên sản phẩm],
       ct.SoLuong AS [Số lượng], FORMAT(ct.DonGia, 'N0', 'vi-VN') + N' đ' AS [Đơn giá],
       FORMAT(ct.ThanhTien, 'N0', 'vi-VN') + N' đ' AS [Thành tiền]
FROM dbo.ChiTietHoaDon ct INNER JOIN dbo.SanPham sp ON ct.MaSP = sp.MaSP
ORDER BY ct.MaHD, ct.MaSP");
            }
        }
    }
}
