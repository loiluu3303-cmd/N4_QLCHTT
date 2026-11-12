using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bỏ qua kiểm tra đăng nhập nếu đang ở trang Login.aspx
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            if (!string.Equals(currentPage, "Login.aspx", StringComparison.OrdinalIgnoreCase))
            {
                if (Session["MaTK"] == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }

            if (!IsPostBack)
            {
                string ten = Convert.ToString(Session["HoTen"]);
                string taiKhoan = Convert.ToString(Session["TenDangNhap"]);
                string quyen = Convert.ToString(Session["Quyen"]);

                string tenHienThi = string.IsNullOrWhiteSpace(ten) ? (string.IsNullOrWhiteSpace(taiKhoan) ? "Tài khoản" : taiKhoan) : ten;

                if (lblTenNguoiDung != null) lblTenNguoiDung.Text = Server.HtmlEncode(tenHienThi);
                if (lblTopbarUser != null) lblTopbarUser.Text = Server.HtmlEncode(tenHienThi);
                if (lblQuyenNguoiDung != null) lblQuyenNguoiDung.Text = Server.HtmlEncode(string.IsNullOrWhiteSpace(quyen) ? "Người dùng" : quyen);
            }
        }
    }
}