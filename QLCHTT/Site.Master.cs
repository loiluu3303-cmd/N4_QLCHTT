using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Tất cả trang dùng Master Page đều yêu cầu đăng nhập.
            if (Session["MaTK"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                string ten = Convert.ToString(Session["HoTen"]);
                string taiKhoan = Convert.ToString(Session["TenDangNhap"]);
                string quyen = Convert.ToString(Session["Quyen"]);

                string tenHienThi = string.IsNullOrWhiteSpace(ten) ? taiKhoan : ten;
                lblTenNguoiDung.Text = Server.HtmlEncode(tenHienThi);
                lblTopbarUser.Text = Server.HtmlEncode(tenHienThi);
                lblQuyenNguoiDung.Text = Server.HtmlEncode(string.IsNullOrWhiteSpace(quyen) ? "Người dùng" : quyen);
            }
        }
    }
}
