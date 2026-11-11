<%@ Page Title="Quản lý tồn kho" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TonKho.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.TonKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Quản lý tồn kho</h1>
        <p>Theo dõi số lượng hàng hóa tồn kho và thông tin sản phẩm.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Toolbar tìm kiếm và lọc -->
    <div class="toolbar">
        <asp:TextBox ID="txtTuKhoa" runat="server" CssClass="form-control search-box" placeholder="Tìm kiếm theo tên sản phẩm, mã..." />
        <asp:Button ID="btnTimKiem" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
        <asp:Button ID="btnLamMoi" runat="server" CssClass="btn btn-outline-secondary ms-auto" Text="Làm mới" OnClick="btnLamMoi_Click" />
    </div>

    <!-- Thông báo -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

    <!-- Bảng dữ liệu GridView hiển thị từ bảng SanPham -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu sản phẩm nào."
            EmptyDataRowStyle-CssClass="db-empty" />
    </div>
    <div class="table-foot">Dữ liệu trong bảng được đọc trực tiếp từ bảng <b>SanPham</b> trong SQL Server database QLCHTT.</div>
</div>
</asp:Content>