using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public static class DuLieu
    {

        private static Control TimControl(Control parent, string id)
        {
            if (parent == null) return null;
            if (string.Equals(parent.ID, id, StringComparison.OrdinalIgnoreCase))
                return parent;

            foreach (Control child in parent.Controls)
            {
                Control found = TimControl(child, id);
                if (found != null) return found;
            }
            return null;
        }

        // Đọc dữ liệu từ SQL Server và tự đóng kết nối sau khi đọc.
        public static DataTable LayBang(string sql)
        {
            KetNoi kn = new KetNoi();
            DataTable bang = new DataTable();

            try
            {
                kn.MoKetNoi();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, kn.GetConnection()))
                {
                    adapter.Fill(bang);
                }
                return bang;
            }
            finally
            {
                kn.DongKetNoi();
            }
        }


        public static void GanGiaTri(Page trang, string idLabel, string sql)
        {
            ContentPlaceHolder noiDung = TimControl(trang.Master, "MainContent") as ContentPlaceHolder;
            if (noiDung == null) return;

            Label nhan = TimControl(noiDung, idLabel) as Label;
            if (nhan == null) return;

            KetNoi kn = new KetNoi();
            try
            {
                kn.MoKetNoi();
                using (SqlCommand cmd = new SqlCommand(sql, kn.GetConnection()))
                {
                    object giaTri = cmd.ExecuteScalar();
                    nhan.Text = (giaTri == null || giaTri == DBNull.Value) ? "0" : Convert.ToString(giaTri);
                }
            }
            catch (Exception ex)
            {
                nhan.Text = "—";
                Label thongBao = TimControl(noiDung, "lblThongBao") as Label;
            if (thongBao == null)
                thongBao = TimControl(noiDung, "lblThongBaoChiTiet") as Label;
                if (thongBao != null)
                {
                    thongBao.Text = "Không tải được dữ liệu thống kê: " + trang.Server.HtmlEncode(ex.Message);
                    thongBao.Visible = true;
                }
            }
            finally
            {
                kn.DongKetNoi();
            }
        }

        // Gắn dữ liệu vào GridView có ID nằm trong MainContent.
        public static void GanGrid(Page trang, string idGrid, string sql)
        {
            ContentPlaceHolder noiDung = TimControl(trang.Master, "MainContent") as ContentPlaceHolder;
            if (noiDung == null) return;

            GridView grid = TimControl(noiDung, idGrid) as GridView;
            Label thongBao = TimControl(noiDung, "lblThongBao") as Label;
            if (thongBao == null)
                thongBao = TimControl(noiDung, "lblThongBaoChiTiet") as Label;

            try
            {
                if (grid == null) return;
                grid.DataSource = LayBang(sql);
                grid.DataBind();
            }
            catch (Exception ex)
            {
                if (thongBao != null)
                {
                    thongBao.Text = "Không tải được dữ liệu. Kiểm tra SQL Server, database QLCHTT và chuỗi kết nối. Chi tiết: " + trang.Server.HtmlEncode(ex.Message);
                    thongBao.Visible = true;
                }
            }
        }
    }
}
