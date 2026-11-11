using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DonHang : Page
    {
        // Khai báo thủ công các Control để tránh lỗi context
        protected global::System.Web.UI.WebControls.Button btnTaoDonHang;
        protected global::System.Web.UI.WebControls.TextBox txtTimKiem;
        protected global::System.Web.UI.WebControls.DropDownList ddlTrangThai;
        protected global::System.Web.UI.WebControls.Button btnLoc;
        protected global::System.Web.UI.WebControls.Label lblThongBao;
        protected global::System.Web.UI.WebControls.GridView gvData;
        protected global::System.Web.UI.WebControls.Label lblThongBaoChiTiet;
        protected global::System.Web.UI.WebControls.GridView gvChiTiet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachHoaDon();
                LoadChiTietHoaDon("HD005"); // Mặc định hiển thị chi tiết hóa đơn đầu tiên
            }
        }

        // Tạo dữ liệu mẫu danh sách hóa đơn (có thể thay bằng kết nối SQL của bạn)
        private DataTable GetHoaDonData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHoaDon");
            dt.Columns.Add("NgayLap", typeof(DateTime));
            dt.Columns.Add("KhachHang");
            dt.Columns.Add("NhanVien");
            dt.Columns.Add("TongTien", typeof(decimal));
            dt.Columns.Add("TrangThai");
            dt.Columns.Add("KhuyenMai");

            dt.Rows.Add("HD005", new DateTime(2026, 10, 5), "Hoàng Nam Khánh", "Vũ Hoàng Oanh", 290000, "Đã thanh toán", "Không áp dụng");
            dt.Rows.Add("HD004", new DateTime(2026, 10, 4), "Phạm Hoàng Yến", "Trần Thị Hoa", 640000, "Chờ thanh toán", "Không áp dụng");
            dt.Rows.Add("HD003", new DateTime(2026, 10, 3), "Lê Ngọc Anh", "Trần Thị Hoa", 420000, "Đã thanh toán", "Không áp dụng");
            dt.Rows.Add("HD002", new DateTime(2026, 10, 2), "Trần Văn Minh", "Nguyễn Văn Nam", 700000, "Đã thanh toán", "Không áp dụng");
            dt.Rows.Add("HD001", new DateTime(2026, 10, 1), "Nguyễn Thị Lan", "Nguyễn Văn Nam", 400000, "Đã thanh toán", "Không áp dụng");

            return dt;
        }

        // Tạo dữ liệu mẫu chi tiết hóa đơn
        private DataTable GetChiTietData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHoaDon");
            dt.Columns.Add("MaSP");
            dt.Columns.Add("TenSP");
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("DonGia", typeof(decimal));
            dt.Columns.Add("ThanhTien", typeof(decimal));

            dt.Rows.Add("HD001", "SP001", "Áo thun cổ tròn", 1, 150000, 150000);
            dt.Rows.Add("HD001", "SP002", "Áo sơ mi nữ", 1, 250000, 250000);
            dt.Rows.Add("HD002", "SP003", "Quần jean ống đứng", 2, 350000, 700000);
            dt.Rows.Add("HD003", "SP004", "Váy hoa nhí", 1, 420000, 420000);
            dt.Rows.Add("HD004", "SP001", "Áo thun cổ tròn", 2, 150000, 300000);
            dt.Rows.Add("HD004", "SP002", "Áo sơ mi nữ", 1, 340000, 340000);
            dt.Rows.Add("HD005", "SP005", "Túi đeo chéo", 1, 290000, 290000);

            return dt;
        }

        private void LoadDanhSachHoaDon(string tuKhoa = "", string trangThai = "")
        {
            DataTable dt = GetHoaDonData();
            DataView dv = dt.DefaultView;

            string filterExpression = "";
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                filterExpression += $"MaHoaDon LIKE '%{tuKhoa}%' OR KhachHang LIKE '%{tuKhoa}%'";
            }

            if (!string.IsNullOrEmpty(trangThai))
            {
                if (!string.IsNullOrEmpty(filterExpression)) filterExpression += " AND ";
                filterExpression += $"TrangThai = '{trangThai}'";
            }

            dv.RowFilter = filterExpression;
            gvData.DataSource = dv;
            gvData.DataBind();
        }

        private void LoadChiTietHoaDon(string maHoaDon)
        {
            DataTable dt = GetChiTietData();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"MaHoaDon = '{maHoaDon}'";

            gvChiTiet.DataSource = dv;
            gvChiTiet.DataBind();
        }

        // Sự kiện lọc dữ liệu khi bấm nút "Lọc" hoặc gõ tìm kiếm
        protected void btnLoc_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            string trangThai = ddlTrangThai.SelectedValue;
            LoadDanhSachHoaDon(tuKhoa, trangThai);
        }

        // Sự kiện click vào Mã hóa đơn để xem Chi tiết hóa đơn tương ứng
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "XemChiTiet")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string maHD = gvData.DataKeys[rowIndex].Value.ToString();
                LoadChiTietHoaDon(maHD);
            }
        }

        // Sự kiện nút chuyển sang trang Tạo đơn hàng
        protected void btnTaoDonHang_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaoHoaDon.aspx");
        }
    }
}
