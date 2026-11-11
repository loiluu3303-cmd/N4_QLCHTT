<%@ Page Title="Đơn hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DonHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DonHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading"><div><h1>Quản lý đơn hàng</h1><p>Theo dõi hóa đơn bán hàng, thanh toán và đổi trả.</p></div><button type="button" class="btn btn-success">+ Tạo đơn hàng</button></div>
    <div class="panel-card"><div class="toolbar"><input class="form-control search-box" placeholder="Tìm mã hóa đơn hoặc khách hàng..." /><select class="form-select filter-box"><option>Tất cả trạng thái</option><option>Đã thanh toán</option><option>Chờ thanh toán</option><option>Đã hủy</option></select><button class="btn btn-outline-secondary">Lọc</button></div>
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />
          <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" />
          </div></div>
    <div class="panel-card mt-3">
        <div class="panel-header"><div><h5>Chi tiết hóa đơn</h5><p>Sản phẩm, số lượng và thành tiền theo từng hóa đơn</p></div></div>
        <asp:Label ID="lblThongBaoChiTiet" runat="server" CssClass="db-message" Visible="false" />
        <div class="table-responsive"><asp:GridView ID="gvChiTiet" runat="server" CssClass="table db-grid" GridLines="None" AutoGenerateColumns="true" EmptyDataText="Chưa có chi tiết hóa đơn." /></div>
    </div>
</asp:Content>
