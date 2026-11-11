<%@ Page Title="Kiểm kê kho" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KiemKe.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KiemKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Kiểm kê kho</h1>
        <p>Kiểm tra số lượng thực tế và cập nhật trực tiếp tồn kho sản phẩm.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Toolbar tìm kiếm -->
    <div class="toolbar">
        <asp:TextBox ID="txtTuKhoa" runat="server" CssClass="form-control search-box" placeholder="Tìm kiếm theo mã, tên sản phẩm..." />
        <asp:Button ID="btnTimKiem" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
        <asp:Button ID="btnLamMoi" runat="server" CssClass="btn btn-outline-secondary ms-auto" Text="Làm mới" OnClick="btnLamMoi_Click" />
    </div>

    <!-- Form cập nhật tồn kho thực tế khớp với bảng SanPham -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-6">
            <label class="form-label">Chọn sản phẩm cần kiểm kê</label>
            <asp:DropDownList ID="ddlMaSP" runat="server" CssClass="form-control" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Số lượng thực tế đếm được</label>
            <asp:TextBox ID="txtSoLuongThucTe" runat="server" CssClass="form-control" TextMode="Number" Text="0" />
        </div>
    </div>

    <!-- Nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnLuu" runat="server" CssClass="btn btn-success" Text="Cập nhật tồn kho" OnClick="btnLuu_Click" />
    </div>

    <!-- Thông báo -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

    <!-- Bảng hiển thị danh sách tồn kho khớp với SanPham ở dưới -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu sản phẩm nào."
            EmptyDataRowStyle-CssClass="db-empty" />
    </div>
    <div class="table-foot">Dữ liệu tồn kho đồng bộ trực tiếp với bảng <b>SanPham</b> trong SQL Server database QLCHTT.</div>
</div>
</asp:Content>
