using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DanhMuc : System.Web.UI.Page
    {
        // Khai báo tường minh các control để tránh triệt để lỗi không tìm thấy trong context
        protected TextBox txtSearch;
        protected TextBox txtMaLoai;
        protected TextBox txtTenLoai;
        protected TextBox txtMoTa;
        protected GridView gvData;
        protected Label lblThongBao;

        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM LoaiSanPham WHERE 1=1";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (MaLoai LIKE @keyword OR TenLoai LIKE @keyword)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
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
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi tải danh mục: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch != null ? txtSearch.Text.Trim() : "";
            LoadData(keyword);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(tenLoai))
            {
                ShowMessage("Vui lòng nhập Mã loại và Tên loại sản phẩm!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM LoaiSanPham WHERE MaLoai = @MaLoai";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Cập nhật
                            string updateQuery = "UPDATE LoaiSanPham SET TenLoai = @TenLoai, MoTa = @MoTa WHERE MaLoai = @MaLoai";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                                cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Cập nhật danh mục thành công!", true);
                        }
                        else
                        {
                            // Thêm mới
                            string insertQuery = "INSERT INTO LoaiSanPham (MaLoai, TenLoai, MoTa) VALUES (@MaLoai, @TenLoai, @MoTa)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                                cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Thêm danh mục thành công!", true);
                        }
                    }
                }

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi lưu danh mục: " + ex.Message, false);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            if (string.IsNullOrEmpty(maLoai))
            {
                ShowMessage("Vui lòng chọn danh mục cần xóa từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM LoaiSanPham WHERE MaLoai = @MaLoai";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Xóa danh mục thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa (Đang có sản phẩm thuộc danh mục này): " + ex.Message, false);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            if (txtSearch != null) txtSearch.Text = "";
            LoadData();
            if (lblThongBao != null) lblThongBao.Visible = false;
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvData != null && gvData.SelectedRow != null)
            {
                GridViewRow row = gvData.SelectedRow;
                txtMaLoai.Text = HttpUtility.HtmlDecode(row.Cells[1].Text).Trim();
                txtTenLoai.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
                txtMoTa.Text = row.Cells.Count > 3 && HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() != "&nbsp;" ? HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() : "";

                txtMaLoai.ReadOnly = true;
                if (lblThongBao != null) lblThongBao.Visible = false;
            }
        }

        private void ClearForm()
        {
            txtMaLoai.Text = "";
            txtMaLoai.ReadOnly = false;
            txtTenLoai.Text = "";
            txtMoTa.Text = "";
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            if (lblThongBao != null)
            {
                lblThongBao.Text = message;
                lblThongBao.Visible = true;
                lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold mb-3" : "db-message text-danger fw-bold mb-3";
            }
        }
    }
}