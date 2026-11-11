using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KhachHang : System.Web.UI.Page
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
                    string query = "SELECT * FROM KhachHang";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " WHERE MaKH LIKE @keyword OR HoTen LIKE @keyword OR SDT LIKE @keyword";
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
                ShowMessage("Lỗi tải dữ liệu: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        // Thêm mới khách hàng
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            int diem = int.TryParse(txtDiemTichLuy.Text.Trim(), out int d) ? d : 0;
            string hangTV = string.IsNullOrEmpty(txtHangThanhVien.Text.Trim()) ? "Thường" : txtHangThanhVien.Text.Trim();

            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(hoTen))
            {
                ShowMessage("Vui lòng nhập Mã khách hàng và Họ tên!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaKH", maKH);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            ShowMessage("Mã khách hàng này đã tồn tại!", false);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO KhachHang (MaKH, HoTen, SDT, DiaChi, DiemTichLuy, HangThanhVien) VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @DiemTichLuy, @HangThanhVien)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                        cmd.Parameters.AddWithValue("@DiemTichLuy", diem);
                        cmd.Parameters.AddWithValue("@HangThanhVien", hangTV);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Thêm khách hàng thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi thêm: " + ex.Message, false);
            }
        }

        // Cập nhật thông tin khách hàng
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            int diem = int.TryParse(txtDiemTichLuy.Text.Trim(), out int d) ? d : 0;
            string hangTV = txtHangThanhVien.Text.Trim();

            if (string.IsNullOrEmpty(maKH))
            {
                ShowMessage("Vui lòng chọn khách hàng cần cập nhật từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE KhachHang SET HoTen = @HoTen, SDT = @SDT, DiaChi = @DiaChi, DiemTichLuy = @DiemTichLuy, HangThanhVien = @HangThanhVien WHERE MaKH = @MaKH";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                        cmd.Parameters.AddWithValue("@DiemTichLuy", diem);
                        cmd.Parameters.AddWithValue("@HangThanhVien", string.IsNullOrEmpty(hangTV) ? (object)DBNull.Value : hangTV);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Cập nhật thông tin thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi cập nhật: " + ex.Message, false);
            }
        }

        // Xóa khách hàng
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                ShowMessage("Vui lòng chọn khách hàng cần xóa từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Xóa khách hàng thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa (Khách hàng này đã có hóa đơn mua hàng liên quan): " + ex.Message, false);
            }
        }

        // Chọn dòng trên bảng để đưa lên form sửa
        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvData.SelectedRow;
            txtMaKH.Text = row.Cells[1].Text.Trim();
            txtHoTen.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
            txtSDT.Text = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();
            txtDiaChi.Text = HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[4].Text).Trim();
            txtDiemTichLuy.Text = HttpUtility.HtmlDecode(row.Cells[5].Text).Trim();
            txtHangThanhVien.Text = HttpUtility.HtmlDecode(row.Cells[6].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[6].Text).Trim();

            txtMaKH.ReadOnly = true;
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
            txtMaKH.Text = "";
            txtMaKH.ReadOnly = false;
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtDiemTichLuy.Text = "0";
            txtHangThanhVien.Text = "";
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold mb-3" : "db-message text-danger fw-bold mb-3";
        }
    }
}