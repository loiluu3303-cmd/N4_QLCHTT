using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KhuyenMai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT MaKM AS [Mã khuyến mãi], TenKM AS [Tên chương trình],
       CAST(PhanTramGiam AS varchar(20)) + '%' AS [Mức giảm],
       CONVERT(varchar(10), NgayBatDau, 103) AS [Ngày bắt đầu],
       CONVERT(varchar(10), NgayKetThuc, 103) AS [Ngày kết thúc],
       TrangThai AS [Trạng thái]
FROM dbo.KhuyenMai ORDER BY NgayBatDau DESC");
            }
        }
    }
}
