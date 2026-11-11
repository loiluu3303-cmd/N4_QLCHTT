using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace QLCHTT_WebForms_Frontend
{
    public partial class SanPham : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoryDropdowns();
                LoadData();
            }
        }

        // Tải danh mục vào các DropDownList
        private void LoadCategoryDropdowns()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT MaLoai, TenLoai FROM LoaiSanPham";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // DropDownList lọc dữ liệu
                            ddlFilterLoai.DataSource = dt;
                            ddlFilterLoai.DataTextField = "TenLoai";
                            ddlFilterLoai.DataValueField = "MaLoai";
                            ddlFilterLoai.DataBind();
                            ddlFilterLoai.Items.Insert(0, new ListItem("-- Tất cả danh mục --", ""));

                            // DropDownList nhập liệu sản phẩm
                            ddlMaLoai.DataSource = dt;
                            ddlMaLoai.DataTextField = "TenLoai";
                            ddlMaLoai.DataValueField = "MaLoai";
                            ddlMaLoai.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi tải danh mục: " + ex.Message, false);
            }
        }

        // Tải danh sách sản phẩm kết hợp tên loại
        private void LoadData(string keyword = "", string maLoaiFilter = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"SELECT s.*, l.TenLoai 
                                     FROM SanPham s 
                                     INNER JOIN LoaiSanPham l ON s.MaLoai = l.MaLoai 
                                     WHERE 1=1";

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (s.MaSP LIKE @keyword OR s.TenSP LIKE @keyword OR s.MauSac LIKE @keyword)";
                    }

                    if (!string.IsNullOrEmpty(maLoaiFilter))
                    {
                        query += " AND s.MaLoai = @maLoai";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        }
                        if (!string.IsNullOrEmpty(maLoaiFilter))
                        {
                            cmd.Parameters.AddWithValue("@maLoai", maLoaiFilter);
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
                ShowMessage("Lỗi tải dữ liệu sản phẩm: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            string maLoai = ddlFilterLoai.SelectedValue;
            LoadData(keyword, maLoai);
        }

        // Thêm mới sản phẩm
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            string tenSP = txtTenSP.Text.Trim();
            string maLoai = ddlMaLoai.SelectedValue;
            decimal donGia = decimal.TryParse(txtDonGia.Text.Trim(), out decimal dg) ? dg : 0;
            int soLuong = int.TryParse(txtSoLuong.Text.Trim(), out int sl) ? sl : 0;
            string kichThuoc = txtKichThuoc.Text.Trim();
            string mauSac = txtMauSac.Text.Trim();
            string trangThai = ddlTrangThai.SelectedValue;

            if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP))
            {
                ShowMessage("Vui lòng nhập Mã sản phẩm và Tên sản phẩm!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaSP", maSP);
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            ShowMessage("Mã sản phẩm này đã tồn tại trong hệ thống!", false);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO SanPham (MaSP, TenSP, MaLoai, DonGia, SoLuong, KichThuoc, MauSac, TrangThai) 
                                         VALUES (@MaSP, @TenSP, @MaLoai, @DonGia, @SoLuong, @KichThuoc, @MauSac, @TrangThai)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.Parameters.AddWithValue("@TenSP", tenSP);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@KichThuoc", string.IsNullOrEmpty(kichThuoc) ? (object)DBNull.Value : kichThuoc);
                        cmd.Parameters.AddWithValue("@MauSac", string.IsNullOrEmpty(mauSac) ? (object)DBNull.Value : mauSac);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Thêm sản phẩm thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi thêm sản phẩm: " + ex.Message, false);
            }
        }

        // Cập nhật sản phẩm
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            string tenSP = txtTenSP.Text.Trim();
            string maLoai = ddlMaLoai.SelectedValue;
            decimal donGia = decimal.TryParse(txtDonGia.Text.Trim(), out decimal dg) ? dg : 0;
            int soLuong = int.TryParse(txtSoLuong.Text.Trim(), out int sl) ? sl : 0;
            string kichThuoc = txtKichThuoc.Text.Trim();
            string mauSac = txtMauSac.Text.Trim();
            string trangThai = ddlTrangThai.SelectedValue;

            if (string.IsNullOrEmpty(maSP))
            {
                ShowMessage("Vui lòng chọn sản phẩm cần cập nhật từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE SanPham 
                                         SET TenSP = @TenSP, MaLoai = @MaLoai, DonGia = @DonGia, SoLuong = @SoLuong, 
                                             KichThuoc = @KichThuoc, MauSac = @MauSac, TrangThai = @TrangThai 
                                         WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.Parameters.AddWithValue("@TenSP", tenSP);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmd.Parameters.AddWithValue("@KichThuoc", string.IsNullOrEmpty(kichThuoc) ? (object)DBNull.Value : kichThuoc);
                        cmd.Parameters.AddWithValue("@MauSac", string.IsNullOrEmpty(mauSac) ? (object)DBNull.Value : mauSac);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Cập nhật sản phẩm thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi cập nhật sản phẩm: " + ex.Message, false);
            }
        }

        // Xóa sản phẩm
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            if (string.IsNullOrEmpty(maSP))
            {
                ShowMessage("Vui lòng chọn sản phẩm cần xóa từ bảng!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.ExecuteNonQuery();
                    }
                }

                ShowMessage("Xóa sản phẩm thành công!", true);
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Không thể xóa (Sản phẩm này đã có trong chi tiết hóa đơn hoặc phiếu nhập kho): " + ex.Message, false);
            }
        }

        // Chọn dòng trên bảng đưa lên form
        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvData.SelectedRow;
            txtMaSP.Text = row.Cells[1].Text.Trim();
            txtTenSP.Text = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();

            // Lấy tên danh mục để chọn đúng dropdown
            string tenLoai = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();
            if (ddlMaLoai.Items.FindByText(tenLoai) != null)
            {
                ddlMaLoai.ClearSelection();
                ddlMaLoai.Items.FindByText(tenLoai).Selected = true;
            }

            // Xử lý giá tiền (bỏ dấu "đ" và dấu chấm phân cách)
            string rawGia = row.Cells[4].Text.Replace("đ", "").Replace(".", "").Trim();
            txtDonGia.Text = rawGia;

            txtSoLuong.Text = row.Cells[5].Text.Trim();
            txtKichThuoc.Text = HttpUtility.HtmlDecode(row.Cells[6].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[6].Text).Trim();
            txtMauSac.Text = HttpUtility.HtmlDecode(row.Cells[7].Text).Trim() == "&nbsp;" ? "" : HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();

            string trangThai = HttpUtility.HtmlDecode(row.Cells[8].Text).Trim();
            if (ddlTrangThai.Items.FindByValue(trangThai) != null)
            {
                ddlTrangThai.SelectedValue = trangThai;
            }

            txtMaSP.ReadOnly = true;
            lblThongBao.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtSearch.Text = "";
            ddlFilterLoai.SelectedIndex = 0;
            LoadData();
            lblThongBao.Visible = false;
        }

        private void ClearForm()
        {
            txtMaSP.Text = "";
            txtMaSP.ReadOnly = false;
            txtTenSP.Text = "";
            if (ddlMaLoai.Items.Count > 0) ddlMaLoai.SelectedIndex = 0;
            txtDonGia.Text = "0";
            txtSoLuong.Text = "0";
            txtKichThuoc.Text = "";
            txtMauSac.Text = "";
            ddlTrangThai.SelectedIndex = 0;
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isSuccess ? "db-message text-success fw-bold mb-3" : "db-message text-danger fw-bold mb-3";
        }
    }
}