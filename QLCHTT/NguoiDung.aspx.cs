using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NguoiDung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/NhanVien.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
