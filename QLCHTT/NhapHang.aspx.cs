using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhapHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT pn.MaPN AS [Mã phiếu nhập], ncc.TenNCC AS [Nhà cung cấp],
       CONVERT(varchar(10), pn.NgayNhap, 103) AS [Ngày nhập],
       nv.HoTen AS [Nhân viên nhập],
       FORMAT(pn.TongTien, 'N0', 'vi-VN') + N' đ' AS [Tổng tiền],
       ISNULL(sp.TenSP, N'Chưa gắn sản phẩm') AS [Sản phẩm]
FROM dbo.PhieuNhapKho pn
INNER JOIN dbo.NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
INNER JOIN dbo.NhanVien nv ON pn.MaNV = nv.MaNV
LEFT JOIN dbo.SanPham sp ON pn.MaSP = sp.MaSP
ORDER BY pn.NgayNhap DESC, pn.MaPN");
            }
        }
    }
}
