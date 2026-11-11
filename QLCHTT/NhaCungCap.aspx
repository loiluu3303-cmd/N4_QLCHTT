<%@ Page Title="Nhà cung cấp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhaCungCap.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhaCungCap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Nhà cung cấp</h1><p>Quản lý thông tin đối tác cung cấp sản phẩm.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Thêm nhà cung cấp</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Tên nhà cung cấp</label><input class="form-control" placeholder="Nhập tên nhà cung cấp" /></div><div class="col-12 col-md-6"><label class="form-label">Số điện thoại</label><input class="form-control" placeholder="Nhập số điện thoại" /></div><div class="col-12 col-md-6"><label class="form-label">Email</label><input class="form-control" placeholder="Nhập email" /></div><div class="col-12 col-md-6"><label class="form-label">Địa chỉ</label><input class="form-control" placeholder="Nhập địa chỉ" /></div></div>
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
