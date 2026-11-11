using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class TaiKhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT tk.MaTK AS [Mã tài khoản], tk.TenDangNhap AS [Tên đăng nhập],
       nv.HoTen AS [Nhân viên], tk.Quyen AS [Quyền]
FROM dbo.TaiKhoan tk INNER JOIN dbo.NhanVien nv ON tk.MaNV = nv.MaNV
ORDER BY tk.MaTK");
            }
        }
    }
}
