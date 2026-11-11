<%@ Page Title="Tổng quan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QLCHTT_WebForms_Frontend._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading">
        <div><h1>Tổng quan</h1><p>Chào mừng bạn đến với hệ thống quản lý cửa hàng thời trang.</p></div>
        <span class="date-chip">Năm 2026</span>
    </div>
    <div class="row g-3 mb-4">
        <div class="col-12 col-sm-6 col-xl-3"><div class="stat-card"><div class="stat-label">Tổng sản phẩm</div><div class="stat-value"><asp:Label ID="lblTongSanPham" runat="server" /></div><div class="stat-note">Sản phẩm đang quản lý</div><div class="stat-icon purple">◇</div></div></div>
        <div class="col-12 col-sm-6 col-xl-3"><div class="stat-card"><div class="stat-label">Đơn hàng</div><div class="stat-value"><asp:Label ID="lblSoHoaDon" runat="server" /></div><div class="stat-note">Tổng số hóa đơn</div><div class="stat-icon blue">▤</div></div></div>
        <div class="col-12 col-sm-6 col-xl-3"><div class="stat-card"><div class="stat-label">Khách hàng</div><div class="stat-value"><asp:Label ID="lblSoKhachHang" runat="server" /></div><div class="stat-note">Thành viên cửa hàng</div><div class="stat-icon orange">♙</div></div></div>
        <div class="col-12 col-sm-6 col-xl-3"><div class="stat-card"><div class="stat-label">Doanh thu</div><div class="stat-value"><asp:Label ID="lblDoanhThu" runat="server" /></div><div class="stat-note">Doanh thu đã thanh toán</div><div class="stat-icon green">₫</div></div></div>
    </div>
    <div class="row g-3">
        <div class="col-12 col-xl-8"><div class="panel-card"><div class="panel-header"><div><h5>Đơn hàng gần đây</h5><p>Danh sách hóa đơn mới nhất từ database</p></div><a href="DonHang.aspx" class="btn btn-sm btn-outline-success">Xem tất cả</a></div>
            <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" /><div class="table-responsive"><asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None" AutoGenerateColumns="true" EmptyDataText="Chưa có hóa đơn." /></div></div></div>
        <div class="col-12 col-xl-4"><div class="panel-card"><div class="panel-header"><div><h5>Tình trạng kho</h5><p>Theo dõi số lượng sản phẩm</p></div></div>
            <asp:GridView ID="gvTonKho" runat="server" CssClass="table db-grid" GridLines="None" AutoGenerateColumns="true" EmptyDataText="Chưa có sản phẩm." /></div>
        </div></div>
    </div>
</asp:Content>
