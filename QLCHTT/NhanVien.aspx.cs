using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhanVien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT MaNV AS [Mã nhân viên], HoTen AS [Họ tên],
       CONVERT(varchar(10), NgaySinh, 103) AS [Ngày sinh],
       SDT AS [Số điện thoại], DiaChi AS [Địa chỉ], ChucVu AS [Chức vụ]
FROM dbo.NhanVien ORDER BY MaNV");
            }
        }
    }
}
