using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KhachHang : System.Web.UI.Page
    {
        // Khai báo tường minh các control để tránh triệt để lỗi không tìm thấy trong context
        protected TextBox txtSearch;
        protected TextBox txtMaKH;
        protected TextBox txtHoTen;
        protected TextBox txtSDT;
        protected TextBox txtDiaChi;
        protected TextBox txtDiemTichLuy;
        protected TextBox txtHangThanhVien;
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
                    string query = "SELECT * FROM KhachHang WHERE 1=1";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (MaKH LIKE @keyword OR HoTen LIKE @keyword OR SDT LIKE @keyword)";
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
                ShowMessage("Lỗi tải danh sách khách hàng: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch != null ? txtSearch.Text.Trim() : "";
            LoadData(keyword);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            int diem = int.TryParse(txtDiemTichLuy.Text.Trim(), out int d) ? d : 0;
            string hang = string.IsNullOrEmpty(txtHangThanhVien.Text.Trim()) ? "Thành viên mới" : txtHangThanhVien.Text.Trim();

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
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Cập nhật nếu đã tồn tại
                            string updateQuery = "UPDATE KhachHang SET HoTen = @HoTen, SDT = @SDT, DiaChi = @DiaChi, DiemTichLuy = @DiemTichLuy, HangThanhVien = @HangThanhVien WHERE MaKH = @MaKH";
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", maKH);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                                cmd.Parameters.AddWithValue("@DiemTichLuy", diem);
                                cmd.Parameters.AddWithValue("@HangThanhVien", hang);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Cập nhật thông tin khách hàng thành công!", true);
                        }
                        else
                        {
                            // Thêm mới nếu chưa có
                            string insertQuery = "INSERT INTO KhachHang (MaKH, HoTen, SDT, DiaChi, DiemTichLuy, HangThanhVien) VALUES (@MaKH, @HoTen, @SDT, @DiaChi, @DiemTichLuy, @HangThanhVien)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", maKH);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi);
                                cmd.Parameters.AddWithValue("@DiemTichLuy", diem);
                                cmd.Parameters.AddWithValue("@HangThanhVien", hang);
                                cmd.ExecuteNonQuery();
                            }
                            ShowMessage("Thêm khách hàng thành công!", true);
                        }
                    }
                }

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi lưu khách hàng: " + ex.Message, false);
            }
        }

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
                ShowMessage("Không thể xóa (Khách hàng này đã có phát sinh hóa đơn): " + ex.Message, false);
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
                txtMaKH.Text = HttpUtility.HtmlDecode(row.Cells[1].Text).Trim();
                txtHoTen.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
                txtSDT.Text = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();

                // Nếu cột địa chỉ hoặc điểm nằm ở cell tiếp theo tương ứng với GridView columns
                txtDiaChi.Text = row.Cells.Count > 4 && HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() != "&nbsp;" ? HttpUtility.HtmlDecode(row.Cells[4].Text).Trim() : "";
                txtDiemTichLuy.Text = row.Cells.Count > 5 ? HttpUtility.HtmlDecode(row.Cells[5].Text).Trim() : "0";
                txtHangThanhVien.Text = row.Cells.Count > 6 ? HttpUtility.HtmlDecode(row.Cells[6].Text).Trim() : "Thành viên mới";

                txtMaKH.ReadOnly = true;
                if (lblThongBao != null) lblThongBao.Visible = false;
            }
        }

        private void ClearForm()
        {
            txtMaKH.Text = "";
            txtMaKH.ReadOnly = false;
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtDiemTichLuy.Text = "0";
            txtHangThanhVien.Text = "Thành viên mới";
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