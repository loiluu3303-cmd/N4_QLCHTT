<%@ Page Title="Quản lý khuyến mãi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KhuyenMai.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KhuyenMai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Quản lý khuyến mãi</h1><p>Quản lý chương trình ưu đãi và mã giảm giá.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Thêm khuyến mãi</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Tên chương trình</label><input class="form-control" placeholder="Nhập tên chương trình" /></div><div class="col-12 col-md-6"><label class="form-label">Mã khuyến mãi</label><input class="form-control" placeholder="Nhập mã" /></div><div class="col-12 col-md-6"><label class="form-label">Loại giảm giá</label><input class="form-control" placeholder="Phần trăm / Số tiền" /></div><div class="col-12 col-md-6"><label class="form-label">Giá trị</label><input class="form-control" placeholder="Nhập giá trị" /></div><div class="col-12 col-md-6"><label class="form-label">Ngày bắt đầu</label><input class="form-control" placeholder="dd/mm/yyyy" /></div><div class="col-12 col-md-6"><label class="form-label">Ngày kết thúc</label><input class="form-control" placeholder="dd/mm/yyyy" /></div></div>
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
