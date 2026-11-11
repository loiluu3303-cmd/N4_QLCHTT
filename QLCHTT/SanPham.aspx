<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SanPham.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.SanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading"><div><h1>Quản lý sản phẩm</h1><p>Quản lý danh sách, danh mục, màu sắc và kích thước sản phẩm.</p></div><button type="button" class="btn btn-success">+ Thêm sản phẩm</button></div>
    <div class="panel-card">
        <div class="toolbar"><input class="form-control search-box" placeholder="Tìm theo mã hoặc tên sản phẩm..." /><select class="form-select filter-box"><option>Tất cả danh mục</option><option>Áo</option><option>Quần</option><option>Váy đầm</option><option>Phụ kiện</option><option>Giày dép</option></select><button class="btn btn-outline-secondary">Lọc</button></div>
        <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />
          <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" />
          </div>
        <div class="table-foot">Hiển thị dữ liệu minh họa. Chức năng thêm, sửa, xóa sẽ được kết nối backend sau.</div>
    </div>
</asp:Content>
