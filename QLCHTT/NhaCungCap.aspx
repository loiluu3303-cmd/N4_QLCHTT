<%@ Page Title="Quản lý nhà cung cấp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhaCungCap.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhaCungCap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Nhà cung cấp</h1>
        <p>Quản lý thông tin đối tác cung cấp sản phẩm.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Khu vực tìm kiếm và nút thêm mới -->
    <div class="toolbar mb-3 d-flex gap-2">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control search-box w-auto" placeholder="Tìm kiếm theo mã, tên, SĐT, email..." />
        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnSearch_Click" />
        <asp:Button ID="btnThemmoi" runat="server" CssClass="btn btn-success ms-auto" Text="+ Thêm nhà cung cấp" OnClick="btnRefresh_Click" />
    </div>

    <!-- Khung nhập liệu (Thêm / Sửa) -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-4">
            <label class="form-label">Mã nhà cung cấp</label>
            <asp:TextBox ID="txtMaNCC" runat="server" CssClass="form-control" placeholder="NCC01" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Tên nhà cung cấp</label>
            <asp:TextBox ID="txtTenNCC" runat="server" CssClass="form-control" placeholder="Nhập tên nhà cung cấp" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Số điện thoại</label>
            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" placeholder="Nhập số điện thoại" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Nhập email" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Địa chỉ</label>
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" placeholder="Nhập địa chỉ" />
        </div>
    </div>

    <!-- Các nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Lưu (Thêm mới)" OnClick="btnSave_Click" />
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhà cung cấp này không?');" />
        <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-outline-secondary ms-auto" Text="Làm mới" OnClick="btnRefresh_Click" />
    </div>

    <!-- Thông báo trạng thái -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />

    <!-- Bảng hiển thị dữ liệu từ SQL Server -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu nhà cung cấp."
            EmptyDataRowStyle-CssClass="db-empty"
            OnSelectedIndexChanged="gvData_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Chọn" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
            </Columns>
        </asp:GridView>
    </div>
    
    <div class="table-foot mt-3 text-muted">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
</div>
</asp:Content>
