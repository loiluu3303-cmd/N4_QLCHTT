using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhapHang : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNgayNhap.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtMaPN.Text = TaoMaPNTuDong();
                LoadDropdowns();
                LoadGridData();
            }
        }

        // Tạo mã phiếu nhập tự động tăng tiếp theo (Ví dụ: PN006)
        private string TaoMaPNTuDong()
        {
            string maMoi = "PN001";
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"]?.ConnectionString
                              ?? "Data Source=MAY04GD3\\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaPN FROM dbo.PhieuNhapKho ORDER BY MaPN DESC", conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string maCu = result.ToString(); // VD: PN005
                        if (maCu.StartsWith("PN") && int.TryParse(maCu.Substring(2), out int stt))
                        {
                            maMoi = "PN" + (stt + 1).ToString("D3");
                        }
                    }
                }
            }
            return maMoi;
        }

        // Đổ dữ liệu cho các DropDownList từ CSDL
        private void LoadDropdowns()
        {
            try
            {
                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"]?.ConnectionString
                                  ?? "Data Source=MAY04GD3\\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Nhà cung cấp
                    using (SqlCommand cmd = new SqlCommand("SELECT MaNCC, TenNCC FROM dbo.NhaCungCap", conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlMaNCC.DataSource = dt;
                        ddlMaNCC.DataTextField = "TenNCC";
                        ddlMaNCC.DataValueField = "MaNCC";
                        ddlMaNCC.DataBind();
                        ddlMaNCC.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Chọn nhà cung cấp --", ""));
                    }

                    // 2. Nhân viên
                    using (SqlCommand cmd = new SqlCommand("SELECT MaNV, HoTen FROM dbo.NhanVien", conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlMaNV.DataSource = dt;
                        ddlMaNV.DataTextField = "HoTen";
                        ddlMaNV.DataValueField = "MaNV";
                        ddlMaNV.DataBind();
                        ddlMaNV.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Chọn nhân viên --", ""));
                    }

                    // 3. Sản phẩm
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
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi tải danh mục: " + ex.Message, true);
            }
        }

        // Tải dữ liệu GridView
        private void LoadGridData(string tuKhoa = "")
        {
            string query = @"SELECT pn.MaPN AS [Mã phiếu nhập], 
                                    ncc.TenNCC AS [Nhà cung cấp],
                                    CONVERT(varchar(10), pn.NgayNhap, 103) AS [Ngày nhập],
                                    nv.HoTen AS [Nhân viên nhập],
                                    FORMAT(pn.TongTien, 'N0', 'vi-VN') + N' đ' AS [Tổng tiền],
                                    ISNULL(sp.TenSP, N'Chưa gắn sản phẩm') AS [Sản phẩm]
                             FROM dbo.PhieuNhapKho pn
                             INNER JOIN dbo.NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                             INNER JOIN dbo.NhanVien nv ON pn.MaNV = nv.MaNV
                             LEFT JOIN dbo.SanPham sp ON pn.MaSP = sp.MaSP";

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += $" WHERE pn.MaPN LIKE N'%{tuKhoa}%' OR ncc.TenNCC LIKE N'%{tuKhoa}%'";
            }

            query += " ORDER BY pn.NgayNhap DESC, pn.MaPN DESC";

            DuLieu.GanGrid(this, "gvData", query);
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadGridData(txtTuKhoa.Text.Trim());
        }

        protected void btnTaoMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            txtTuKhoa.Text = "";
            LoadGridData();
        }

        // Lưu phiếu nhập vào bảng PhieuNhapKho
        protected void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaPN.Text.Trim()))
                {
                    HienThiThongBao("Vui lòng nhập Mã phiếu nhập!", true);
                    return;
                }

                if (ddlMaNCC.SelectedValue == "" || ddlMaNV.SelectedValue == "" || ddlMaSP.SelectedValue == "")
                {
                    HienThiThongBao("Vui lòng chọn đầy đủ Nhà cung cấp, Nhân viên và Sản phẩm!", true);
                    return;
                }

                decimal tongTien = decimal.Parse(txtTongTien.Text);
                DateTime ngayNhap = DateTime.Parse(txtNgayNhap.Text);

                string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"]?.ConnectionString
                                  ?? "Data Source=MAY04GD3\\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO dbo.PhieuNhapKho (MaPN, NgayNhap, MaNCC, MaNV, TongTien, MaSP) 
                                           VALUES (@MaPN, @NgayNhap, @MaNCC, @MaNV, @TongTien, @MaSP)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", txtMaPN.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                        cmd.Parameters.AddWithValue("@MaNCC", ddlMaNCC.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaNV", ddlMaNV.SelectedValue);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@MaSP", ddlMaSP.SelectedValue);

                        cmd.ExecuteNonQuery();
                    }
                }

                HienThiThongBao("Tạo và lưu phiếu nhập thành công!", false);
                LoadGridData();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                HienThiThongBao("Lỗi khi lưu dữ liệu (Trùng mã phiếu hoặc sai định dạng): " + ex.Message, true);
            }
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
            LoadGridData();
            HienThiThongBao("Đã làm mới dữ liệu.", false);
        }

        private void LamMoiForm()
        {
            txtMaPN.Text = TaoMaPNTuDong();
            if (ddlMaNCC.Items.Count > 0) ddlMaNCC.SelectedIndex = 0;
            if (ddlMaNV.Items.Count > 0) ddlMaNV.SelectedIndex = 0;
            if (ddlMaSP.Items.Count > 0) ddlMaSP.SelectedIndex = 0;
            txtNgayNhap.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtTongTien.Text = "0";
        }

        private void HienThiThongBao(string msg, bool isError)
        {
            lblThongBao.Text = msg;
            lblThongBao.Visible = true;
            lblThongBao.CssClass = isError ? "db-message alert alert-danger d-block" : "db-message alert alert-success d-block";
        }
    }
}
