<%@ Page Title="Tài khoản & phân quyền" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaiKhoan.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.TaiKhoan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Tài khoản & phân quyền</h1><p>Giao diện quản lý tài khoản đăng nhập và vai trò người dùng.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Tạo tài khoản</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Tên đăng nhập</label><input class="form-control" placeholder="Nhập tên đăng nhập" /></div><div class="col-12 col-md-6"><label class="form-label">Mật khẩu</label><input class="form-control" placeholder="Nhập mật khẩu" /></div><div class="col-12 col-md-6"><label class="form-label">Nhân viên</label><input class="form-control" placeholder="Chọn nhân viên" /></div><div class="col-12 col-md-6"><label class="form-label">Vai trò</label><input class="form-control" placeholder="Quản trị / Nhân viên" /></div><div class="col-12 col-md-6"><label class="form-label">Trạng thái</label><input class="form-control" placeholder="Hoạt động" /></div></div>
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
