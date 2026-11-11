<%@ Page Title="Danh mục sản phẩm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhMuc.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DanhMuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Danh mục sản phẩm</h1>
        <p>Quản lý nhóm và loại sản phẩm thời trang.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Toolbar tìm kiếm -->
    <div class="toolbar mb-3 d-flex gap-2">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control search-box w-auto" placeholder="Tìm kiếm..." />
        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnSearch_Click" />
    </div>

    <!-- Khung nhập liệu (Thêm / Sửa) -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-4">
            <label class="form-label">Mã loại sản phẩm</label>
            <asp:TextBox ID="txtMaLoai" runat="server" CssClass="form-control" placeholder="LSP01" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Tên loại sản phẩm</label>
            <asp:TextBox ID="txtTenLoai" runat="server" CssClass="form-control" placeholder="Áo / Quần / Váy đầm" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Mô tả</label>
            <asp:TextBox ID="txtMoTa" runat="server" CssClass="form-control" placeholder="Mô tả danh mục" />
        </div>
    </div>

    <!-- Các nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Thêm mới" OnClick="btnSave_Click" />
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa loại sản phẩm này không?');" />
        <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-outline-secondary ms-auto" Text="Làm mới" OnClick="btnRefresh_Click" />
    </div>

    <!-- Thông báo trạng thái lỗi hoặc thành công -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message mb-3 d-block" Visible="false" />

    <!-- Bảng hiển thị dữ liệu -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu cho mục này."
            EmptyDataRowStyle-CssClass="db-empty" 
            OnSelectedIndexChanged="gvData_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Chọn" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
            </Columns>
        </asp:GridView>
    </div>
    
    <div class="table-foot mt-3 text-muted">Dữ liệu trong bảng được đọc trực tiếp từ bảng <b>LoaiSanPham</b> trong database QLCHTT.</div>
</div>
</asp:Content>