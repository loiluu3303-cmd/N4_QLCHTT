<%@ Page Title="Đổi trả hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoiTraHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DoiTraHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading">
        <div>
            <h1>Đổi trả hàng</h1>
            <p>Theo dõi yêu cầu đổi hoặc trả sản phẩm.</p>
        </div>
    </div>

    <div class="panel-card">
        <!-- Khu vực tìm kiếm và nút thêm -->
        <div class="toolbar">
            <asp:TextBox ID="txtTimKiem" runat="server" CssClass="form-control search-box" placeholder="Tìm kiếm theo mã, khách hàng, sản phẩm, lý do..." />
            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="btn btn-outline-secondary" OnClick="btnTimKiem_Click" />
            <asp:Button ID="btnThemYeuCauTop" runat="server" Text="Thêm yêu cầu đổi trả" CssClass="btn btn-success ms-auto" OnClick="btnThemYeuCauTop_Click" />
        </div>

        <!-- Form nhập thông tin đổi trả (Đầy đủ các khung nhập) -->
        <div class="row g-3 mb-3">
            <div class="col-12 col-md-6">
                <label class="form-label">Mã hóa đơn</label>
                <asp:TextBox ID="txtMaHoaDon" runat="server" CssClass="form-control" placeholder="Nhập mã hóa đơn" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Khách hàng</label>
                <asp:TextBox ID="txtKhachHang" runat="server" CssClass="form-control" placeholder="Nhập tên khách hàng" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Sản phẩm</label>
                <asp:TextBox ID="txtSanPham" runat="server" CssClass="form-control" placeholder="Nhập tên sản phẩm" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Loại yêu cầu</label>
                <asp:DropDownList ID="ddlLoaiYeuCau" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Đổi hàng" Value="Đổi hàng" />
                    <asp:ListItem Text="Trả hàng" Value="Trả hàng" />
                </asp:DropDownList>
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Lý do</label>
                <asp:TextBox ID="txtLyDo" runat="server" CssClass="form-control" placeholder="Nhập lý do" />
            </div>
            <div class="col-12 col-md-6">
                <label class="form-label">Tổng tiền hoàn</label>
                <asp:TextBox ID="txtTongTienHoan" runat="server" CssClass="form-control" TextMode="Number" placeholder="Nhập số tiền hoàn" />
            </div>
        </div>

        <!-- Các nút chức năng -->
        <div class="d-flex gap-2 mb-3">
            <asp:Button ID="btnLuu" runat="server" Text="Lưu yêu cầu" CssClass="btn btn-success" OnClick="btnLuu_Click" />
            <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" CssClass="btn btn-outline-secondary" OnClick="btnLamMoi_Click" />
        </div>

        <!-- Thông báo -->
        <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

        <!-- Lưới dữ liệu (Đã có cột Sản phẩm và Mã đổi trả) -->
        <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="false" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty">
                <Columns>
                    <asp:BoundField DataField="MaDoiTra" HeaderText="Mã đổi trả" />
                    <asp:BoundField DataField="MaHoaDon" HeaderText="Mã hóa đơn" />
                    <asp:BoundField DataField="SanPham" HeaderText="Sản phẩm" />
                    <asp:BoundField DataField="KhachHang" HeaderText="Khách hàng" />
                    <asp:BoundField DataField="NgayDoiTra" HeaderText="Ngày đổi trả" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="LyDo" HeaderText="Lý do" />
                    <asp:BoundField DataField="TongTienHoan" HeaderText="Tổng tiền hoàn" DataFormatString="{0:N0} ₫" />
                    <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="table-foot">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
    </div>
</asp:Content>