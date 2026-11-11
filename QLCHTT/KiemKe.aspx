<%@ Page Title="Kiểm kê kho" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KiemKe.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KiemKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Kiểm kê kho</h1><p>Danh sách tồn kho lấy từ bảng SanPham. Database hiện chưa có bảng lưu lịch sử kiểm kê.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Tạo đợt kiểm kê</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Mã kiểm kê</label><input class="form-control" placeholder="KK001" /></div><div class="col-12 col-md-6"><label class="form-label">Ngày kiểm kê</label><input class="form-control" placeholder="09/11/2026" /></div><div class="col-12 col-md-6"><label class="form-label">Người kiểm kê</label><input class="form-control" placeholder="Administrator" /></div><div class="col-12 col-md-6"><label class="form-label">Sản phẩm</label><input class="form-control" placeholder="Chọn sản phẩm" /></div><div class="col-12 col-md-6"><label class="form-label">Số lượng thực tế</label><input class="form-control" placeholder="0" /></div><div class="col-12 col-md-6"><label class="form-label">Ghi chú</label><input class="form-control" placeholder="Nhập ghi chú" /></div></div>
          <div class="d-flex gap-2 mb-3"><button type="button" class="btn btn-success">Lưu (giao diện)</button><button type="button" class="btn btn-outline-secondary">Làm mới</button></div>
          <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />
          <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" />
          </div>
          <div class="table-foot">Dữ liệu tồn kho đọc từ SanPham; database chưa có bảng KiemKe để lưu các đợt kiểm kê.</div>
        </div>
</asp:Content>
