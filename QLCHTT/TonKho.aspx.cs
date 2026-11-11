using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace QLCHTT_WebForms_Frontend
{
    public partial class TonKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridData();
            }
        }

        // Tải danh sách tồn kho từ bảng SanPham kết hợp LoaiSanPham
        private void LoadGridData(string tuKhoa = "")
        {
            string query = @"SELECT sp.MaSP AS [Mã sản phẩm], 
                            sp.TenSP AS [Tên sản phẩm],
                            lsp.TenLoai AS [Loại sản phẩm],
                            FORMAT(sp.DonGia, 'N0', 'vi-VN') + N' đ' AS [Đơn giá],
                            sp.SoLuong AS [Số lượng tồn],
                            ISNULL(sp.KichThuoc, N'N/A') AS [Kích thước],
                            ISNULL(sp.MauSac, N'N/A') AS [Màu sắc],
                            ISNULL(sp.TrangThai, N'Đang bán') AS [Trang thái]
                     FROM dbo.SanPham sp
                     INNER JOIN dbo.LoaiSanPham lsp ON sp.MaLoai = lsp.MaLoai";

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                // Cho phép tìm kiếm không phân biệt hoa thường và tìm theo mã hoặc tên
                query += $" WHERE sp.MaSP LIKE N'%{tuKhoa}%' OR sp.TenSP LIKE N'%{tuKhoa}%'";
            }

            query += " ORDER BY sp.MaSP DESC";

            try
            {
                DuLieu.GanGrid(this, "gvData", query);
            }
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi tải dữ liệu tồn kho: " + ex.Message, true);
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadGridData(txtTuKhoa.Text.Trim());
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            LoadGridData();
            HienThiThongBao("Đã làm mới danh sách tồn kho.", false);
        }

        private void HienThiThongBao(string msg, bool isError)
        {
            lblThongBao.Text = msg;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isError ? "db-message alert alert-danger d-block" : "db-message alert alert-success d-block";
        }
    }
}
