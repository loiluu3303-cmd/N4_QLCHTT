using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DoiTraHang : Page
    {
        // Khai báo thủ công các Control để tránh lỗi context
        protected global::System.Web.UI.WebControls.TextBox txtTimKiem;
        protected global::System.Web.UI.WebControls.Button btnTimKiem;
        protected global::System.Web.UI.WebControls.Button btnThemYeuCauTop;
        protected global::System.Web.UI.WebControls.TextBox txtMaHoaDon;
        protected global::System.Web.UI.WebControls.TextBox txtKhachHang;
        protected global::System.Web.UI.WebControls.TextBox txtSanPham;
        protected global::System.Web.UI.WebControls.DropDownList ddlLoaiYeuCau;
        protected global::System.Web.UI.WebControls.TextBox txtLyDo;
        protected global::System.Web.UI.WebControls.TextBox txtTongTienHoan;
        protected global::System.Web.UI.WebControls.Button btnLuu;
        protected global::System.Web.UI.WebControls.Button btnLamMoi;
        protected global::System.Web.UI.WebControls.Label lblThongBao;
        protected global::System.Web.UI.WebControls.GridView gvData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachDoiTra();
            }
        }

        // Tạo dữ liệu mẫu tương ứng giao diện (có thể thay thế bằng câu lệnh kết nối SQL của bạn)
        private DataTable GetDoiTraTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDoiTra");
            dt.Columns.Add("MaHoaDon");
            dt.Columns.Add("KhachHang");
            dt.Columns.Add("NgayDoiTra", typeof(DateTime));
            dt.Columns.Add("LyDo");
            dt.Columns.Add("TongTienHoan", typeof(decimal));
            dt.Columns.Add("TrangThai");

            dt.Rows.Add("DT005", "HD005", "Hoàng Nam Khánh", new DateTime(2026, 10, 5), "Lỗi kích cỡ", 290000, "Đã hoàn tiền");
            dt.Rows.Add("DT004", "HD004", "Phạm Hoàng Yến", new DateTime(2026, 10, 6), "Khách đổi ý không mua nữa", 290000, "Đã hủy");
            dt.Rows.Add("DT001", "HD003", "Lê Ngọc Anh", new DateTime(2026, 10, 5), "Khách đổi kích thước", 420000, "Đã hoàn tiền");
            dt.Rows.Add("DT003", "HD002", "Trần Văn Minh", new DateTime(2026, 10, 3), "Khách đổi màu sắc khác", 350000, "Đang xử lý");
            dt.Rows.Add("DT002", "HD001", "Nguyễn Thị Lan", new DateTime(2026, 10, 2), "Sản phẩm bị lỗi chỉ", 150000, "Đã hoàn tiền");

            return dt;
        }

        private void LoadDanhSachDoiTra(string tuKhoa = "")
        {
            DataTable dt = GetDoiTraTable();
            DataView dv = dt.DefaultView;

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dv.RowFilter = $"MaDoiTra LIKE '%{tuKhoa}%' OR MaHoaDon LIKE '%{tuKhoa}%' OR KhachHang LIKE '%{tuKhoa}%' OR LyDo LIKE '%{tuKhoa}%'";
            }

            gvData.DataSource = dv;
            gvData.DataBind();
        }

        // Sự kiện tìm kiếm
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            LoadDanhSachDoiTra(tuKhoa);
        }

        // Nút thêm nhanh trên header
        protected void btnThemYeuCauTop_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Focus();
            HienThongBao("Vui lòng nhập thông tin yêu cầu đổi trả ở form bên dưới rồi bấm Lưu.", true);
        }

        // Sự kiện Lưu yêu cầu đổi trả mới
        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHoaDon.Text.Trim();
            string khachHang = txtKhachHang.Text.Trim();
            string lyDo = txtLyDo.Text.Trim();

            if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(khachHang) || string.IsNullOrEmpty(lyDo))
            {
                HienThongBao("Vui lòng nhập đầy đủ Mã hóa đơn, Khách hàng và Lý do!", false);
                return;
            }

            if (!decimal.TryParse(txtTongTienHoan.Text, out decimal tienHoan))
            {
                tienHoan = 0;
            }

            // TODO: Viết lệnh SQL INSERT vào cơ sở dữ liệu QLCHTT bảng Đổi Trả ở đây.

            HienThongBao($"Thêm thành công yêu cầu đổi trả cho hóa đơn {maHD}!", true);
            btnLamMoi_Click(sender, e);
            LoadDanhSachDoiTra();
        }

        // Sự kiện làm mới form
        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = "";
            txtKhachHang.Text = "";
            txtSanPham.Text = "";
            txtLyDo.Text = "";
            txtTongTienHoan.Text = "";
            ddlLoaiYeuCau.SelectedIndex = 0;
            lblThongBao.Visible = false;
        }

        private void HienThongBao(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.CssClass = isSuccess ? "db-message alert alert-success d-block" : "db-message alert alert-danger d-block";
            lblThongBao.Visible = true;
        }
    }
}