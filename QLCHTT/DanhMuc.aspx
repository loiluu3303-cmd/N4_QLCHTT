<%@ Page Title="Danh mục sản phẩm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhMuc.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DanhMuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Danh mục sản phẩm</h1><p>Quản lý nhóm và loại sản phẩm thời trang.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Thêm danh mục</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Mã danh mục</label><input class="form-control" placeholder="DM001" /></div><div class="col-12 col-md-6"><label class="form-label">Tên danh mục</label><input class="form-control" placeholder="Áo / Quần / Váy đầm" /></div><div class="col-12 col-md-6"><label class="form-label">Mô tả</label><input class="form-control" placeholder="Mô tả danh mục" /></div></div>
          <div class="d-flex gap-2 mb-3"><button type="button" class="btn btn-success">Lưu (giao diện)</button><button type="button" class="btn btn-outline-secondary">Làm mới</button></div>
          <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />
          <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" />
          </div>
          <div class="table-foot">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
        </div>
</asp:Content>
