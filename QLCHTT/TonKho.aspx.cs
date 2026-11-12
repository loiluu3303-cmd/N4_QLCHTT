using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class TonKho : System.Web.UI.Page
    {
        // Khai báo tường minh các control để khớp hoàn toàn và chống lỗi context
        protected TextBox txtTuKhoa;
        protected Button btnTimKiem;
        protected Button btnLamMoi;
        protected GridView gvData;
        protected Label lblThongBao;

        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridData();
            }
        }

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
                query += " WHERE sp.MaSP LIKE @tuKhoa OR sp.TenSP LIKE @tuKhoa";
            }

            query += " ORDER BY sp.MaSP DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(tuKhoa))
                        {
                            cmd.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa + "%");
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (gvData != null)
                            {
                                gvData.DataSource = dt;
                                gvData.DataBind();
                            }
                        }
                    }
                }

                if (lblThongBao != null) lblThongBao.Visible = false;
            }
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi tải dữ liệu tồn kho: " + ex.Message, true);
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTuKhoa != null ? txtTuKhoa.Text.Trim() : "";
            LoadGridData(tuKhoa);
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (txtTuKhoa != null) txtTuKhoa.Text = "";
            LoadGridData();
            HienThiThongBao("Đã làm mới danh sách tồn kho.", false);
        }

        private void HienThiThongBao(string msg, bool isError)
        {
            if (lblThongBao != null)
            {
                lblThongBao.Text = msg;
                lblThongBao.Visible = true;
                lblThongBao.CssClass = isError ? "db-message alert alert-danger d-block" : "db-message alert alert-success d-block";
            }
        }
    }
}