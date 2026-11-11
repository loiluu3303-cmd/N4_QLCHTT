using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DoiTraHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT dt.MaDT AS [Mã đổi trả], dt.MaHD AS [Mã hóa đơn],
       kh.HoTen AS [Khách hàng], CONVERT(varchar(10), dt.NgayDoiTra, 103) AS [Ngày đổi trả],
       dt.LyDo AS [Lý do], FORMAT(ISNULL(dt.TongTienHoan, 0), 'N0', 'vi-VN') + N' đ' AS [Tổng tiền hoàn],
       dt.TrangThai AS [Trạng thái]
FROM dbo.DoiTraHang dt
INNER JOIN dbo.HoaDon hd ON dt.MaHD = hd.MaHD
INNER JOIN dbo.KhachHang kh ON hd.MaKH = kh.MaKH
ORDER BY dt.NgayDoiTra DESC, dt.MaDT");
            }
        }
    }
}
