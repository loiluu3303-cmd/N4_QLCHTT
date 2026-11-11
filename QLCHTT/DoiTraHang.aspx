<%@ Page Title="Đổi trả hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoiTraHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DoiTraHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading"><div><h1>Đổi trả hàng</h1><p>Theo dõi yêu cầu đổi hoặc trả sản phẩm.</p></div></div>

        <div class="panel-card">
          <div class="toolbar"><input class="form-control search-box" placeholder="Tìm kiếm..." /><button type="button" class="btn btn-outline-secondary">Tìm kiếm</button><button type="button" class="btn btn-success ms-auto">Thêm yêu cầu đổi trả</button></div>
          <div class="row g-3 mb-3"><div class="col-12 col-md-6"><label class="form-label">Mã hóa đơn</label><input class="form-control" placeholder="Nhập mã hóa đơn" /></div><div class="col-12 col-md-6"><label class="form-label">Khách hàng</label><input class="form-control" placeholder="Chọn khách hàng" /></div><div class="col-12 col-md-6"><label class="form-label">Sản phẩm</label><input class="form-control" placeholder="Chọn sản phẩm" /></div><div class="col-12 col-md-6"><label class="form-label">Loại yêu cầu</label><input class="form-control" placeholder="Đổi hàng / Trả hàng" /></div><div class="col-12 col-md-6"><label class="form-label">Lý do</label><input class="form-control" placeholder="Nhập lý do" /></div></div>
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
