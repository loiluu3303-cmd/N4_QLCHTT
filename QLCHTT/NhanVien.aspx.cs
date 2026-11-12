using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class NhanVien : System.Web.UI.Page
    {
        // Khai báo tường minh các control để chống triệt để lỗi mất context
        protected TextBox txtTimKiem;
        protected GridView gvData;
        protected Label lblThongBao;

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

                            if (gvData != null)
                            {
                                gvData.DataSource = dt;
                                gvData.DataBind();
                            }
                        }
                    }
                }

                if (lblThongBao != null) lblThongBao.Visible = false;
            }
            catch (Exception ex)
            {
                if (lblThongBao != null)
                {
                    lblThongBao.Text = "Lỗi tải danh sách nhân viên: " + Server.HtmlEncode(ex.Message);
                    lblThongBao.Visible = true;
                }
            }
        }

        // Tìm kiếm nhân viên
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem != null ? txtTimKiem.Text : "";
            LoadNhanVien(keyword);
        }

        // Làm mới danh sách
        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (txtTimKiem != null) txtTimKiem.Text = "";
            LoadNhanVien("");
        }
    }
}