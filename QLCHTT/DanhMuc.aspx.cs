using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DanhMuc : System.Web.UI.Page
    {
        // Tự động đọc chuỗi kết nối từ Web.config
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
                    string query = "SELECT * FROM LoaiSanPham";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " WHERE MaLoai LIKE @keyword OR TenLoai LIKE @keyword OR MoTa LIKE @keyword";
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

                            gvData.DataSource = dt;
                            gvData.DataBind();
                        }
                    }
                }
                lblThongBao.Visible = false;
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi kết nối hoặc tải dữ liệu: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(tenLoai))
            {
                ShowMessage("Vui lòng nhập đầy đủ Mã loại và Tên loại sản phẩm!", false);
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
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            ShowMessage("Mã loại sản phẩm này đã tồn tại trong hệ thống!", false);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO LoaiSanPham (MaLoai, TenLoai, MoTa) VALUES (@MaLoai, @TenLoai, @MoTa)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                        cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Thêm mới loại sản phẩm thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi thêm: " + ex.Message, false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            string tenLoai = txtTenLoai.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(maLoai))
            {
                ShowMessage("Vui lòng chọn loại sản phẩm cần cập nhật từ bảng phía dưới!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE LoaiSanPham SET TenLoai = @TenLoai, MoTa = @MoTa WHERE MaLoai = @MaLoai";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                        cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Cập nhật thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi cập nhật: " + ex.Message, false);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maLoai = txtMaLoai.Text.Trim();
            if (string.IsNullOrEmpty(maLoai))
            {
                ShowMessage("Vui lòng chọn loại sản phẩm cần xóa từ bảng!", false);
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

                ShowMessage("Xóa thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa (Loại sản phẩm này đang được sử dụng ở bảng Sản phẩm): " + ex.Message, false);
            }
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvData.SelectedRow;
            txtMaLoai.Text = row.Cells[1].Text.Trim();
            txtTenLoai.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();

            string moTaCell = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();
            txtMoTa.Text = (moTaCell == "&nbsp;") ? "" : moTaCell;

            txtMaLoai.ReadOnly = true; // Khóa mã không cho sửa khi đang cập nhật
            lblThongBao.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtSearch.Text = "";
            LoadData();
            lblThongBao.Visible = false;
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
            lblThongBao.Text = message;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold p-2 bg-light border rounded mb-3" : "db-message text-danger fw-bold p-2 bg-light border rounded mb-3";
        }
    }
}
