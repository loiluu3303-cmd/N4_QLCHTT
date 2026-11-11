using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    public partial class DoiTraHang : Page
    {
        // Khai báo thủ công các Control để tránh hoàn toàn lỗi context
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

        // Quản lý dữ liệu bằng ViewState để thêm mới và hiển thị trực tiếp lên GridView
        private DataTable DanhSachDoiTra
        {
            get
            {
                if (ViewState["DT_Data"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MaDoiTra");
                    dt.Columns.Add("MaHoaDon");
                    dt.Columns.Add("SanPham");
                    dt.Columns.Add("KhachHang");
                    dt.Columns.Add("NgayDoiTra", typeof(DateTime));
                    dt.Columns.Add("LyDo");
                    dt.Columns.Add("TongTienHoan", typeof(decimal));
                    dt.Columns.Add("TrangThai");

                    // Dữ liệu mẫu ban đầu khớp với giao diện
                    dt.Rows.Add("DT005", "HD005", "Túi đeo chéo", "Hoàng Nam Khánh", new DateTime(2026, 10, 5), "Lỗi kích cỡ", 290000, "Đã hoàn tiền");
                    dt.Rows.Add("DT004", "HD004", "Áo thun cổ tròn", "Phạm Hoàng Yến", new DateTime(2026, 10, 6), "Khách đổi ý không mua nữa", 290000, "Đã hủy");
                    dt.Rows.Add("DT001", "HD003", "Váy hoa nhí", "Lê Ngọc Anh", new DateTime(2026, 10, 5), "Khách đổi kích thước", 420000, "Đã hoàn tiền");
                    dt.Rows.Add("DT003", "HD002", "Quần jean ống đứng", "Trần Văn Minh", new DateTime(2026, 10, 3), "Khách đổi màu sắc khác", 350000, "Đang xử lý");
                    dt.Rows.Add("DT002", "HD001", "Áo sơ mi nữ", "Nguyễn Thị Lan", new DateTime(2026, 10, 2), "Sản phẩm bị lỗi chỉ", 150000, "Đã hoàn tiền");

                    ViewState["DT_Data"] = dt;
                }
                return (DataTable)ViewState["DT_Data"];
            }
            set
            {
                ViewState["DT_Data"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid(string tuKhoa = "")
        {
            DataTable dt = DanhSachDoiTra;
            DataView dv = dt.DefaultView;

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dv.RowFilter = $"MaDoiTra LIKE '%{tuKhoa}%' OR MaHoaDon LIKE '%{tuKhoa}%' OR SanPham LIKE '%{tuKhoa}%' OR KhachHang LIKE '%{tuKhoa}%' OR LyDo LIKE '%{tuKhoa}%'";
            }

            gvData.DataSource = dv;
            gvData.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            BindGrid(txtTimKiem.Text.Trim());
        }

        protected void btnThemYeuCauTop_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Focus();
            HienThongBao("Vui lòng điền thông tin vào các khung bên dưới rồi bấm Lưu yêu cầu.", true);
        }

        // Sự kiện khi bấm nút Lưu yêu cầu mới lên GridView
        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHoaDon.Text.Trim();
            string khachHang = txtKhachHang.Text.Trim();
            string sanPham = txtSanPham.Text.Trim();
            string lyDo = txtLyDo.Text.Trim();

            if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(khachHang) || string.IsNullOrEmpty(sanPham) || string.IsNullOrEmpty(lyDo))
            {
                HienThongBao("Vui lòng nhập đầy đủ Mã hóa đơn, Khách hàng, Sản phẩm và Lý do!", false);
                return;
            }

            if (!decimal.TryParse(txtTongTienHoan.Text, out decimal tienHoan))
            {
                tienHoan = 0;
            }

            DataTable dt = DanhSachDoiTra;
            string maDTMoi = "DT00" + (dt.Rows.Count + 1);

            // Thêm dòng mới vào danh sách
            dt.Rows.Add(maDTMoi, maHD, sanPham, khachHang, DateTime.Now, lyDo, tienHoan, "Đang xử lý");
            DanhSachDoiTra = dt;

            BindGrid();
            HienThongBao($"Đã lưu thành công yêu cầu đổi trả cho sản phẩm [{sanPham}] (HĐ: {maHD}) lên GridView!", true);

            // Xóa trắng các ô nhập
            btnLamMoi_Click(sender, e);
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHoaDon.Text = "";
            txtKhachHang.Text = "";
            txtSanPham.Text = "";
            txtLyDo.Text = "";
            txtTongTienHoan.Text = "";
            txtTimKiem.Text = "";
            ddlLoaiYeuCau.SelectedIndex = 0;
            lblThongBao.Visible = false;
            BindGrid();
        }

        private void HienThongBao(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.CssClass = isSuccess ? "db-message alert alert-success d-block" : "db-message alert alert-danger d-block";
            lblThongBao.Visible = true;
        }
    }
}
