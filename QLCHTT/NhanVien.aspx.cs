
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhanVien : System.Web.UI.Page
    {
        private readonly string strKetNoi =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True;Encrypt=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNhanVien("");
            }
        }

        // Hiển thị danh sách nhân viên
        private void LoadNhanVien(string tuKhoa)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strKetNoi))
                {
                    string sql = @"
                        SELECT
                            MaNV AS [Mã nhân viên],
                            HoTen AS [Họ tên],
                            CONVERT(varchar(10), NgaySinh, 103) AS [Ngày sinh],
                            SDT AS [Số điện thoại],
                            DiaChi AS [Địa chỉ],
                            ChucVu AS [Chức vụ]
                        FROM NhanVien
                        WHERE
                            MaNV LIKE @TuKhoa
                            OR HoTen LIKE @TuKhoa
                            OR SDT LIKE @TuKhoa
                            OR ChucVu LIKE @TuKhoa
                        ORDER BY MaNV";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@TuKhoa", SqlDbType.NVarChar, 100)
                            .Value = "%" + tuKhoa.Trim() + "%";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            gvData.DataSource = dt;
                            gvData.DataBind();
                        }
                    }
                }

                lblThongBao.Visible = false;
            }
            catch (Exception ex)
            {
                lblThongBao.Text =
                    "Lỗi tải danh sách nhân viên: " + Server.HtmlEncode(ex.Message);

                lblThongBao.Visible = true;
            }
        }

        // Tìm kiếm nhân viên
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadNhanVien(txtTimKiem.Text);
        }

        // Làm mới danh sách
        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadNhanVien("");
        }
    }
}
