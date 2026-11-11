<%@ Page Title="Cài đặt hệ thống" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CaiDat.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.CaiDat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading"><div><h1>Cài đặt hệ thống</h1><p>Các thông tin cấu hình giao diện và cửa hàng.</p></div></div>
    <div class="row g-3"><div class="col-12 col-lg-7"><div class="panel-card"><div class="panel-header"><div><h5>Thông tin cửa hàng</h5><p>Thông tin hiển thị trên hệ thống</p></div></div>
        <div class="mb-3"><label class="form-label">Tên cửa hàng</label><input class="form-control" value="Fashion Store" /></div>
        <div class="mb-3"><label class="form-label">Số điện thoại</label><input class="form-control" placeholder="Nhập số điện thoại" /></div>
        <div class="mb-3"><label class="form-label">Địa chỉ</label><textarea class="form-control" rows="2" placeholder="Nhập địa chỉ cửa hàng"></textarea></div>
        <button type="button" class="btn btn-success">Lưu giao diện mẫu</button>
    </div></div><div class="col-12 col-lg-5"><div class="panel-card"><h5>Thông tin hệ thống</h5><p class="text-muted small">Phiên bản giao diện: 1.0</p><p class="text-muted small">Nền tảng: ASP.NET Web Forms</p><p class="text-muted small">CSS Framework: Bootstrap 5</p><p class="text-muted small mb-0">Backend và kết nối SQL Server sẽ bổ sung ở giai đoạn sau.</p></div></div></div>
</asp:Content>
