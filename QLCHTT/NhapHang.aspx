<%@ Page Title="Nhập hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhapHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhapHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Nhập hàng</h1><p>Quản lý phiếu nhập kho và thông tin nhà cung cấp.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Tạo phiếu nhập</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Nhà cung cấp</label><input class="form-control" placeholder="Chọn nhà cung cấp" /></div><div class="col-12 col-md-6"><label class="form-label">Nhân viên nhập</label><input class="form-control" placeholder="Administrator" /></div><div class="col-12 col-md-6"><label class="form-label">Ngày nhập</label><input class="form-control" placeholder="09/11/2026" /></div><div class="col-12 col-md-6"><label class="form-label">Sản phẩm</label><input class="form-control" placeholder="Tìm sản phẩm" /></div><div class="col-12 col-md-6"><label class="form-label">Số lượng</label><input class="form-control" placeholder="1" /></div><div class="col-12 col-md-6"><label class="form-label">Đơn giá nhập</label><input class="form-control" placeholder="0" /></div></div>
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
