<%@ Page Title="Quản lý nhân viên" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhanVien.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhanVien" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Quản lý nhân viên</h1><p>Quản lý hồ sơ nhân viên làm việc tại cửa hàng.</p></div></div>

        <div class="panel-card">
          <div class="toolbar">
            <asp:TextBox ID="txtTimKiem" runat="server" CssClass="form-control search-box" placeholder="Tìm kiếm..." />
            <asp:Button ID="btnTimKiem" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
            <button type="button" class="btn btn-success ms-auto">Thêm nhân viên</button>
          </div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Họ tên</label><input class="form-control" placeholder="Nhập họ tên" /></div><div class="col-12 col-md-6"><label class="form-label">Số điện thoại</label><input class="form-control" placeholder="Nhập số điện thoại" /></div><div class="col-12 col-md-6"><label class="form-label">Email</label><input class="form-control" placeholder="Nhập email" /></div><div class="col-12 col-md-6"><label class="form-label">Chức vụ</label><input class="form-control" placeholder="Chọn chức vụ" /></div><div class="col-12 col-md-6"><label class="form-label">Địa chỉ</label><input class="form-control" placeholder="Nhập địa chỉ" /></div></div>
          <div class="d-flex gap-2 mb-3"><button type="button" class="btn btn-success">Lưu (giao diện)</button><asp:Button ID="btnLamMoi" runat="server" CssClass="btn btn-outline-secondary" Text="Làm mới" OnClick="btnLamMoi_Click" /></div>
          <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />
          <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" />
          </div>
          <div class="table-foot">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
        </div>
</asp:Content>
