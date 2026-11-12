using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhaCungCap : System.Web.UI.Page
    {
        // Khai báo tường minh để khớp chính xác với txtTimKiem
        protected TextBox txtTimKiem;
        protected TextBox txtMaNCC;
        protected TextBox txtTenNCC;
        protected TextBox txtSDT;
        protected TextBox txtEmail;
        protected TextBox txtDiaChi;
        protected GridView gvData;
        protected Label lblThongBao;

        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadData();
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT * FROM NhaCungCap WHERE 1=1";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (MaNCC LIKE @keyword OR TenNCC LIKE @keyword OR SDT LIKE @keyword)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(keyword))
                            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (gvData != null) { gvData.DataSource = dt; gvData.DataBind(); }
                        }
                    }
                }
            }
            catch (Exception ex) { ShowMessage("Lỗi tải dữ liệu: " + ex.Message, false); }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem != null ? txtTimKiem.Text.Trim() : "";
            LoadData(keyword);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = txtTenNCC.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC))
            {
                ShowMessage("Vui lòng nhập Mã NCC và Tên nhà cung cấp!", false);
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
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            string updateQuery = "UPDATE NhaCungCap SET TenNCC = @TenNCC, SDT = @SDT, Email = @Email, DiaChi = @DiaChi WHERE MaNCC = @MaNCC";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                                cmd.Parameters.AddWithValue("@TenNCC", tenNCC);
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Cập nhật nhà cung cấp thành công!", true);
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO NhaCungCap (MaNCC, TenNCC, SDT, Email, DiaChi) VALUES (@MaNCC, @TenNCC, @SDT, @Email, @DiaChi)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                                cmd.Parameters.AddWithValue("@TenNCC", tenNCC);
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Thêm nhà cung cấp thành công!", true);
                        }
                    }
                }
                LoadData();
                ClearForm();
            }
            catch (Exception ex) { ShowMessage("Lỗi lưu dữ liệu: " + ex.Message, false); }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maNCC = txtMaNCC.Text.Trim();
            if (string.IsNullOrEmpty(maNCC)) { ShowMessage("Vui lòng chọn nhà cung cấp cần xóa!", false); return; }

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
                ShowMessage("Xóa nhà cung cấp thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex) { ShowMessage("Không thể xóa (Đang có phiếu nhập kho từ NCC này): " + ex.Message, false); }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            if (txtTimKiem != null) txtTimKiem.Text = "";
            LoadData();
            if (lblThongBao != null) lblThongBao.Visible = false;
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvData != null && gvData.SelectedRow != null)
            {
                GridViewRow row = gvData.SelectedRow;
                txtMaNCC.Text = HttpUtility.HtmlDecode(row.Cells[1].Text).Trim();
                txtTenNCC.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
                txtSDT.Text = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();
                txtEmail.Text = row.Cells.Count > 4 && HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() != "&nbsp;" ? HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() : "";

                txtMaNCC.ReadOnly = true;
                if (lblThongBao != null) lblThongBao.Visible = false;
            }
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
            if (lblThongBao != null)
            {
                lblThongBao.Text = message;
                lblThongBao.Visible = true;
                lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold mb-3" : "db-message text-danger fw-bold mb-3";
            }
        }
    }
}