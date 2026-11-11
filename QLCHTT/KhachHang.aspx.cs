using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KhachHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT MaKH AS [Mã khách hàng], HoTen AS [Họ tên], SDT AS [Số điện thoại],
       DiaChi AS [Địa chỉ], DiemTichLuy AS [Điểm tích lũy], HangThanhVien AS [Hạng thành viên]
FROM dbo.KhachHang ORDER BY MaKH");
            }
        }
    }
}
