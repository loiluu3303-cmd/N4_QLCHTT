using System;
using System.Data;
using System.Data.SqlClient;

namespace QLCHTT_WebForms_Frontend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaTK"] != null)
            {
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                HienThongBao("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            KetNoi kn = new KetNoi();
            try
            {
                kn.MoKetNoi();

                const string sql = @"
                    SELECT TOP 1 tk.MaTK, tk.TenDangNhap, tk.MaNV, tk.Quyen, nv.HoTen
                    FROM dbo.TaiKhoan AS tk
                    INNER JOIN dbo.NhanVien AS nv ON nv.MaNV = tk.MaNV
                    WHERE tk.TenDangNhap = @TenDangNhap
                      AND tk.MatKhau = @MatKhau;";

                using (SqlCommand cmd = new SqlCommand(sql, kn.GetConnection()))
                {
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = tenDangNhap;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.VarChar, 255).Value = matKhau;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session["MaTK"] = reader["MaTK"].ToString();
                            Session["TenDangNhap"] = reader["TenDangNhap"].ToString();
                            Session["MaNV"] = reader["MaNV"].ToString();
                            Session["Quyen"] = reader["Quyen"] == DBNull.Value ? "" : reader["Quyen"].ToString();
                            Session["HoTen"] = reader["HoTen"].ToString();

                            Response.Redirect("~/Default.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                    }
                }

                HienThongBao("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            catch
            {
                HienThongBao("Không thể kết nối cơ sở dữ liệu. Hãy kiểm tra SQL Server và database QLCHTT.");
            }
            finally
            {
                kn.DongKetNoi();
            }
        }

        private void HienThongBao(string noiDung)
        {
            lblThongBao.Text = noiDung;
        }
    }
}
