<%@ Page Title="Tạo hóa đơn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaoHoaDon.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.TaoHoaDon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading">
        <div>
            <h1>Tạo hóa đơn</h1>
            <p>Giao diện lập hóa đơn bán hàng tại cửa hàng.</p>
        </div>
    </div>

    <div class="row g-3">
        <!-- Panel Thông tin bán hàng -->
        <div class="col-12 col-xl-7">
            <div class="panel-card">
                <div class="panel-header">
                    <div>
                        <h5>Thông tin bán hàng</h5>
                        <p>Thông tin khách hàng và sản phẩm</p>
                    </div>
                </div>

                <div class="row g-3">
                    <div class="col-12 col-md-6">
                        <label class="form-label">Mã hóa đơn</label>
                        <asp:TextBox ID="txtMaHD" runat="server" CssClass="form-control" ReadOnly="true" />
                    </div>
                    <div class="col-12 col-md-6">
                        <label class="form-label">Khách hàng</label>
                        <asp:DropDownList ID="ddlKhachHang" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <label class="form-label">Nhân viên</label>
                        <asp:TextBox ID="txtNhanVien" runat="server" CssClass="form-control" ReadOnly="true" Text="Administrator" />
                    </div>
                    <div class="col-12 col-md-6">
                        <label class="form-label">Sản phẩm</label>
                        <asp:DropDownList ID="ddlSanPham" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <label class="form-label">Số lượng</label>
                        <asp:TextBox ID="txtSoLuong" runat="server" CssClass="form-control" TextMode="Number" Text="1" />
                    </div>
                    <div class="col-12 col-md-6">
                        <label class="form-label">Khuyến mãi (%)</label>
                        <asp:DropDownList ID="ddlKhuyenMai" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlKhuyenMai_SelectedIndexChanged">
                            <asp:ListItem Text="Không áp dụng (0%)" Value="0" />
                            <asp:ListItem Text="Giảm 5%" Value="5" />
                            <asp:ListItem Text="Giảm 10%" Value="10" />
                            <asp:ListItem Text="Giảm 20%" Value="20" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="d-flex gap-2 mt-4">
                    <asp:Button ID="btnThemSP" runat="server" Text="Thêm sản phẩm" CssClass="btn btn-success" OnClick="btnThemSP_Click" />
                    <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" CssClass="btn btn-outline-secondary" OnClick="btnLamMoi_Click" />
                </div>
            </div>
        </div>

        <!-- Panel Thanh toán -->
        <div class="col-12 col-xl-5">
            <div class="panel-card">
                <h5>Thanh toán</h5>
                <div class="invoice-line">
                    <span>Tạm tính</span>
                    <strong><asp:Label ID="lblTamTinh" runat="server" Text="0₫" /></strong>
                </div>
                <div class="invoice-line">
                    <span>Giảm giá</span>
                    <strong><asp:Label ID="lblGiamGia" runat="server" Text="0₫" /></strong>
                </div>
                <div class="invoice-total">
                    <span>Tổng thanh toán</span>
                    <strong><asp:Label ID="lblTongThanhToan" runat="server" Text="0₫" /></strong>
                </div>
                <asp:Button ID="btnLuuHoaDon" runat="server" Text="Lưu hóa đơn" CssClass="btn btn-success w-100 mt-3" OnClick="btnLuuHoaDon_Click" />
            </div>
        </div>
    </div>

    <!-- Danh sách sản phẩm trong giỏ hàng tạm -->
    <div class="panel-card mt-3">
        <div class="panel-header">
            <div>
                <h5>Chi tiết hóa đơn</h5>
                <p>Danh sách sản phẩm được chọn vào hóa đơn hiện tại</p>
            </div>
        </div>

        <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

        <div class="table-responsive">
            <asp:GridView ID="gvGioHang" runat="server" CssClass="table db-grid" AutoGenerateColumns="false" 
                GridLines="None" EmptyDataText="Chưa có sản phẩm được chọn vào hóa đơn." OnRowCommand="gvGioHang_RowCommand">
                <Columns>
                    <asp:BoundField DataField="MaSP" HeaderText="Mã SP" />
                    <asp:BoundField DataField="TenSP" HeaderText="Tên sản phẩm" />
                    <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0} ₫" />
                    <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                    <asp:BoundField DataField="ThanhTien" HeaderText="Thành tiền" DataFormatString="{0:N0} ₫" />
                    <asp:TemplateField HeaderText="Thao tác">
                        <ItemTemplate>
                            <asp:Button ID="btnXoa" runat="server" CommandName="XoaItem" CommandArgument='<%# Container.DataItemIndex %>' Text="Xóa" CssClass="btn btn-sm btn-outline-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>