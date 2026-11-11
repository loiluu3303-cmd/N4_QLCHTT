using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhaCungCap : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.;Initial Catalog=QLCHTT;Integrated Security=True";

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
                    string query = "SELECT * FROM NhaCungCap";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " WHERE MaNCC LIKE @keyword OR TenNCC LIKE @keyword OR SDT LIKE @keyword OR Email LIKE @keyword OR DiaChi LIKE @keyword";
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

        // Thêm mới nhà cung cấp
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC))
            {
                ShowMessage("Vui lòng nhập đầy đủ Mã nhà cung cấp và Tên nhà cung cấp!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            ShowMessage("Mã nhà cung cấp này đã tồn tại trong hệ thống!", false);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO NhaCungCap (MaNCC, TenNCC, SDT, DiaChi, Email) VALUES (@MaNCC, @TenNCC, @SDT, @DiaChi, @Email)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@TenNCC", tenNCC);
                        cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Thêm mới nhà cung cấp thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi thêm: " + ex.Message, false);
            }
        }

        // Cập nhật nhà cung cấp
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC))
            {
                ShowMessage("Vui lòng chọn nhà cung cấp cần cập nhật từ bảng phía dưới!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE NhaCungCap SET TenNCC = @TenNCC, SDT = @SDT, DiaChi = @DiaChi, Email = @Email WHERE MaNCC = @MaNCC";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.Parameters.AddWithValue("@TenNCC", tenNCC);
                        cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
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

        // Xóa nhà cung cấp
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            if (string.IsNullOrEmpty(maNCC))
            {
                ShowMessage("Vui lòng chọn nhà cung cấp cần xóa từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Xóa thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa (Nhà cung cấp này đang có phiếu nhập kho liên quan): " + ex.Message, false);
            }
        }

        // Sự kiện khi bấm "Chọn" trên bảng để đưa dữ liệu lên form sửa
        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvData.SelectedRow;
            txtMaNCC.Text = row.Cells[1].Text.Trim();
            txtTenNCC.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
            txtSDT.Text = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();
            txtEmail.Text = HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[4].Text).Trim();
            txtDiaChi.Text = HttpUtility.HtmlDecode(row.Cells[5].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[5].Text).Trim();

            txtMaNCC.ReadOnly = true; // Khóa mã khi cập nhật
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
            txtMaNCC.Text = "";
            txtMaNCC.ReadOnly = false;
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold mb-3" : "db-message text-danger fw-bold mb-3";
        }
    }
}