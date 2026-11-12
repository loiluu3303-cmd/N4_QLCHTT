using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class KiemKe : System.Web.UI.Page
    {
        // Khai báo tường minh tất cả các control để triệt để hết lỗi mất context
        protected TextBox txtTimKiem;
        protected TextBox txtTuKhoa;
        protected DropDownList ddlMaSP;
        protected TextBox txtSoLuongThucTe;
        protected GridView gvData;
        protected Label lblThongBao;

        string connStr = ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"] != null
            ? ConfigurationManager.ConnectionStrings["QLCHTTConnectionString"].ConnectionString
            : @"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHTT;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadData();
            }
        }

        private void LoadDropdowns()
        {
            try
            {
                if (ddlMaSP != null)
                {
                    ddlMaSP.Items.Clear();
                    ddlMaSP.Items.Add(new ListItem("-- Chọn sản phẩm --", ""));

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "SELECT MaSP, TenSP FROM SanPham";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                foreach (DataRow row in dt.Rows)
                                {
                                    ddlMaSP.Items.Add(new ListItem(row["TenSP"].ToString() + " (" + row["MaSP"].ToString() + ")", row["MaSP"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi tải danh mục sản phẩm: " + ex.Message, false);
            }
        }

        private void LoadData(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT MaSP, TenSP, SoLuong AS TonKho, SoLuong AS ThucTe, 0 AS ChenhLech FROM SanPham WHERE 1=1";
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (MaSP LIKE @keyword OR TenSP LIKE @keyword)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(keyword))
                            cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

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
                ShowMessage("Lỗi tải dữ liệu kiểm kê: " + ex.Message, false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Hỗ trợ linh hoạt cả txtTimKiem hoặc txtTuKhoa
            string keyword = "";
            if (txtTimKiem != null) keyword = txtTimKiem.Text.Trim();
            else if (txtTuKhoa != null) keyword = txtTuKhoa.Text.Trim();

            LoadData(keyword);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string maSP = ddlMaSP != null ? ddlMaSP.SelectedValue : "";
            int.TryParse(txtSoLuongThucTe.Text.Trim(), out int slThucTe);

            if (string.IsNullOrEmpty(maSP))
            {
                ShowMessage("Vui lòng chọn sản phẩm cần kiểm kê!", false);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string updateQuery = "UPDATE SanPham SET SoLuong = @SoLuong WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", slThucTe);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.ExecuteNonQuery();
                    }
                }
                ShowMessage("Cập nhật số lượng kiểm kê kho thành công!", true);
                LoadData();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi cập nhật kho: " + ex.Message, false);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (txtTimKiem != null) txtTimKiem.Text = "";
            if (txtTuKhoa != null) txtTuKhoa.Text = "";
            if (txtSoLuongThucTe != null) txtSoLuongThucTe.Text = "";
            if (ddlMaSP != null && ddlMaSP.Items.Count > 0) ddlMaSP.SelectedIndex = 0;
            if (lblThongBao != null) lblThongBao.Visible = false;
            LoadData();
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