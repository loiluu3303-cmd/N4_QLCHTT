using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLCHTT_WebForms_Frontend
{
    // Model lưu trữ chi tiết tạm của sản phẩm trong hóa đơn
    [Serializable]
    public class ChiTietHoaDonItem
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }

    public partial class TaoHoaDon : Page
    {

        // Giỏ hàng lưu trữ tạm trong ViewState của trang
        private List<ChiTietHoaDonItem> GioHang
        {
            get
            {
                if (ViewState["GioHang"] == null)
                {
                    ViewState["GioHang"] = new List<ChiTietHoaDonItem>();
                }
                return (List<ChiTietHoaDonItem>)ViewState["GioHang"];
            }
            set
            {
                ViewState["GioHang"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TaoMaHoaDonTuDong();
                LoadDanhSachKhachHang();
                LoadDanhSachSanPham();
                CapNhatGioHang();
            }
        }

        private void TaoMaHoaDonTuDong()
        {
            txtMaHD.Text = "HD-" + DateTime.Now.ToString("yyMMddHHmmss");
        }

        private void LoadDanhSachKhachHang()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaKH");
            dt.Columns.Add("TenKH");

            dt.Rows.Add("KH001", "Nguyễn Văn A");
            dt.Rows.Add("KH002", "Trần Thị B");
            dt.Rows.Add("KH003", "Khách lẻ (Khách vãng lai)");

            ddlKhachHang.DataSource = dt;
            ddlKhachHang.DataTextField = "TenKH";
            ddlKhachHang.DataValueField = "MaKH";
            ddlKhachHang.DataBind();
        }

        private void LoadDanhSachSanPham()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSP");
            dt.Columns.Add("TenSP");
            dt.Columns.Add("DonGia", typeof(decimal));

            dt.Rows.Add("SP001", "Áo thun cổ tròn - 150.000₫", 150000);
            dt.Rows.Add("SP002", "Áo sơ mi nữ - 250.000₫", 250000);
            dt.Rows.Add("SP003", "Quần jean ống đứng - 350.000₫", 350000);
            dt.Rows.Add("SP005", "Túi đeo chéo - 290.000₫", 290000);

            ddlSanPham.DataSource = dt;
            ddlSanPham.DataTextField = "TenSP";
            ddlSanPham.DataValueField = "MaSP";
            ddlSanPham.DataBind();
        }

        protected void btnThemSP_Click(object sender, EventArgs e)
        {
            if (ddlSanPham.SelectedItem == null) return;

            string maSP = ddlSanPham.SelectedValue;
            string tenSP = ddlSanPham.SelectedItem.Text.Split('-')[0].Trim();

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                HienThongBao("Số lượng nhập không hợp lệ!", false);
                return;
            }

            decimal donGia = GetDonGiaByMaSP(maSP);

            var danhSach = GioHang;
            var itemTonTai = danhSach.FirstOrDefault(x => x.MaSP == maSP);

            if (itemTonTai != null)
            {
                itemTonTai.SoLuong += soLuong;
            }
            else
            {
                danhSach.Add(new ChiTietHoaDonItem
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    DonGia = donGia,
                    SoLuong = soLuong
                });
            }

            GioHang = danhSach;
            CapNhatGioHang();
            HienThongBao($"Đã thêm {tenSP} vào hóa đơn.", true);
        }

        protected void gvGioHang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "XoaItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var danhSach = GioHang;
                if (index >= 0 && index < danhSach.Count)
                {
                    danhSach.RemoveAt(index);
                    GioHang = danhSach;
                    CapNhatGioHang();
                    HienThongBao("Đã xóa sản phẩm khỏi hóa đơn.", true);
                }
            }
        }

        protected void ddlKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhTienThanhToan();
        }

        private void CapNhatGioHang()
        {
            gvGioHang.DataSource = GioHang;
            gvGioHang.DataBind();
            TinhTienThanhToan();
        }

        private void TinhTienThanhToan()
        {
            decimal tamTinh = GioHang.Sum(x => x.ThanhTien);
            int phanTramGiam = int.Parse(ddlKhuyenMai.SelectedValue);
            decimal giamGia = tamTinh * phanTramGiam / 100;
            decimal tongThanhToan = tamTinh - giamGia;

            lblTamTinh.Text = string.Format("{0:N0} ₫", tamTinh);
            lblGiamGia.Text = string.Format("{0:N0} ₫", giamGia);
            lblTongThanhToan.Text = string.Format("{0:N0} ₫", tongThanhToan);
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            TaoMaHoaDonTuDong();
            txtSoLuong.Text = "1";
            ddlKhuyenMai.SelectedIndex = 0;
            GioHang = new List<ChiTietHoaDonItem>();
            CapNhatGioHang();
            lblThongBao.Visible = false;
        }

        protected void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (GioHang.Count == 0)
            {
                HienThongBao("Chưa có sản phẩm nào trong hóa đơn để lưu!", false);
                return;
            }

            // TODO: Viết code kết nối Database SQL (INSERT bảng HoaDon và ChiTietHoaDon) ở đây.

            HienThongBao($"Lưu hóa đơn {txtMaHD.Text} thành công!", true);
            btnLamMoi_Click(sender, e);
        }

        private decimal GetDonGiaByMaSP(string maSP)
        {
            switch (maSP)
            {
                case "SP001": return 150000;
                case "SP002": return 250000;
                case "SP003": return 350000;
                case "SP005": return 290000;
                default: return 0;
            }
        }

        private void HienThongBao(string message, bool isSuccess)
        {
            lblThongBao.Text = message;
            lblThongBao.CssClass = isSuccess ? "db-message alert alert-success d-block" : "db-message alert alert-danger d-block";
            lblThongBao.Visible = true;
        }
    }
}
