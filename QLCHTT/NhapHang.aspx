<%@ Page Title="Nhập hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhapHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhapHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Nhập hàng</h1>
        <p>Quản lý phiếu nhập kho và thông tin nhà cung cấp.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Toolbar tìm kiếm -->
    <div class="toolbar">
        <asp:TextBox ID="txtTuKhoa" runat="server" CssClass="form-control search-box" placeholder="Tìm kiếm theo mã phiếu, nhà cung cấp..." />
        <asp:Button ID="btnTimKiem" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
        <asp:Button ID="btnTaoMoi" runat="server" CssClass="btn btn-success ms-auto" Text="Tạo phiếu nhập" OnClick="btnTaoMoi_Click" />
    </div>

    <!-- Form nhập liệu khớp với cấu trúc bảng PhieuNhapKho -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-6">
            <label class="form-label">Mã phiếu nhập</label>
            <asp:TextBox ID="txtMaPN" runat="server" CssClass="form-control" placeholder="Ví dụ: PN006" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Ngày nhập</label>
            <asp:TextBox ID="txtNgayNhap" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Nhà cung cấp</label>
            <asp:DropDownList ID="ddlMaNCC" runat="server" CssClass="form-control" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Nhân viên nhập</label>
            <asp:DropDownList ID="ddlMaNV" runat="server" CssClass="form-control" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Sản phẩm</label>
            <asp:DropDownList ID="ddlMaSP" runat="server" CssClass="form-control" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Tổng tiền (VNĐ)</label>
            <asp:TextBox ID="txtTongTien" runat="server" CssClass="form-control" TextMode="Number" Text="0" />
        </div>
    </div>

    <!-- Nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnLuu" runat="server" CssClass="btn btn-success" Text="Lưu phiếu nhập" OnClick="btnLuu_Click" />
        <asp:Button ID="btnLamMoi" runat="server" CssClass="btn btn-outline-secondary" Text="Làm mới" OnClick="btnLamMoi_Click" />
    </div>

    <!-- Thông báo lỗi/thành công -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

    <!-- Bảng dữ liệu GridView -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
            EmptyDataRowStyle-CssClass="db-empty" />
    </div>
    <div class="table-foot">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
</div>
</asp:Content>