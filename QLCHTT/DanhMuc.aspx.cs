using System;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DanhMuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DuLieu.GanGrid(this, "gvData", @"SELECT l.MaLoai AS [Mã danh mục], l.TenLoai AS [Tên danh mục], l.MoTa AS [Mô tả],
       COUNT(sp.MaSP) AS [Số sản phẩm]
FROM dbo.LoaiSanPham l LEFT JOIN dbo.SanPham sp ON l.MaLoai = sp.MaLoai
GROUP BY l.MaLoai, l.TenLoai, l.MoTa ORDER BY l.MaLoai");
            }
        }
    }
}
