using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhaCungCap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT MaNCC AS [Mã nhà cung cấp], TenNCC AS [Tên nhà cung cấp],
       SDT AS [Số điện thoại], Email AS [Email], DiaChi AS [Địa chỉ]
FROM dbo.NhaCungCap ORDER BY MaNCC");
            }
        }
    }
}
