using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KiemKe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadGridData();
            }
        }

        private void LoadDropdowns()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"]?.ConnectionString
                              ?? "Data Source=MAY04GD3\\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT MaSP, TenSP FROM dbo.SanPham", conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlMaSP.DataSource = dt;
                    ddlMaSP.DataTextField = "TenSP";
                    ddlMaSP.DataValueField = "MaSP";
                    ddlMaSP.DataBind();
                    ddlMaSP.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Chọn sản phẩm --", ""));
                }
            }
        }

        private void LoadGridData(string tuKhoa = "")
        {
            string query = @"SELECT sp.MaSP AS [Mã sản phẩm], 
                                    sp.TenSP AS [Tên sản phẩm],
                                    lsp.TenLoai AS [Danh mục],
                                    sp.SoLuong AS [Số lượng tồn thực tế],
                                    ISNULL(sp.MauSac, N'N/A') AS [Màu sắc],
                                    ISNULL(sp.KichThuoc, N'N/A') AS [Kích thước]
                             FROM dbo.SanPham sp
                             INNER JOIN dbo.LoaiSanPham lsp ON sp.MaLoai = lsp.MaLoai";

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += $" WHERE sp.MaSP LIKE N'%{tuKhoa}%' OR sp.TenSP LIKE N'%{tuKhoa}%'";
            }

            query += " ORDER BY sp.MaSP DESC";

            try
            {
                DuLieu.GanGrid(this, "gvData", query);
            }
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi tải danh sách: " + ex.Message, true);
            }
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadGridData(txtTuKhoa.Text.Trim());
        }

        // Cập nhật thẳng số lượng tồn kho vào bảng SanPham
        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMaSP.SelectedValue == "")
                {
                    HienThiThongBao("Vui lòng chọn sản phẩm cần kiểm kê!", true);
                    return;
                }

                int soLuongThucTe = int.Parse(txtSoLuongThucTe.Text);
                string maSP = ddlMaSP.SelectedValue;

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"]?.ConnectionString
                                  ?? "Data Source=MAY04GD3\\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE dbo.SanPham SET SoLuong = @SoLuong WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongThucTe);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.ExecuteNonQuery();
                    }
                }

                HienThiThongBao("Cập nhật số lượng tồn kho thành công!", false);
                LoadGridData();
                ddlMaSP.SelectedIndex = 0;
                txtSoLuongThucTe.Text = "0";
            }
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi khi cập nhật kho: " + ex.Message, true);
            }
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";
            ddlMaSP.SelectedIndex = 0;
            txtSoLuongThucTe.Text = "0";
            LoadGridData();
            HienThiThongBao("Đã làm mới dữ liệu.", false);
        }

        private void HienThiThongBao(string msg, bool isError)
        {
            lblThongBao.Text = msg;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isError ? "db-message alert alert-danger d-block" : "db-message alert alert-success d-block";
        }
    }
}
