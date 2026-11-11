<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SanPham.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.SanPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Quản lý sản phẩm</h1>
        <p>Quản lý danh sách, danh mục, màu sắc và kích thước sản phẩm.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Toolbar tìm kiếm và lọc -->
    <div class="toolbar mb-3 d-flex flex-wrap gap-2 align-items-center">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control search-box w-auto" placeholder="Tìm theo mã hoặc tên sản phẩm..." />
        <asp:DropDownList ID="ddlFilterLoai" runat="server" CssClass="form-select w-auto" AppendDataBoundItems="true">
            <asp:ListItem Value="">-- Tất cả danh mục --</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="Lọc / Tìm kiếm" OnClick="btnSearch_Click" />
        <asp:Button ID="btnThemmoi" runat="server" CssClass="btn btn-success ms-auto" Text="+ Thêm sản phẩm" OnClick="btnRefresh_Click" />
    </div>

    <!-- Khung nhập liệu (Thêm / Sửa sản phẩm) -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-3">
            <label class="form-label">Mã sản phẩm</label>
            <asp:TextBox ID="txtMaSP" runat="server" CssClass="form-control" placeholder="SP001" />
        </div>
        <div class="col-12 col-md-5">
            <label class="form-label">Tên sản phẩm</label>
            <asp:TextBox ID="txtTenSP" runat="server" CssClass="form-control" placeholder="Nhập tên sản phẩm" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Danh mục sản phẩm</label>
            <asp:DropDownList ID="ddlMaLoai" runat="server" CssClass="form-select" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Đơn giá (VNĐ)</label>
            <asp:TextBox ID="txtDonGia" runat="server" CssClass="form-control" Text="0" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Số lượng</label>
            <asp:TextBox ID="txtSoLuong" runat="server" CssClass="form-control" Text="0" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Kích thước</label>
            <asp:TextBox ID="txtKichThuoc" runat="server" CssClass="form-control" placeholder="S / M / L / Free size" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Màu sắc</label>
            <asp:TextBox ID="txtMauSac" runat="server" CssClass="form-control" placeholder="Trắng / Đen / Đỏ..." />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Trạng thái</label>
            <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="form-select">
                <asp:ListItem Value="Đang bán">Đang bán</asp:ListItem>
                <asp:ListItem Value="Tạm ngưng">Tạm ngưng</asp:ListItem>
                <asp:ListItem Value="Hết hàng">Hết hàng</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <!-- Các nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Lưu (Thêm mới)" OnClick="btnSave_Click" />
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?');" />
        <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-outline-secondary ms-auto" Text="Làm mới" OnClick="btnRefresh_Click" />
    </div>

    <!-- Thông báo trạng thái -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message d-block mb-3" Visible="false" />

    <!-- Bảng hiển thị danh sách sản phẩm kết hợp tên loại danh mục -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="false" EmptyDataText="Database chưa có dữ liệu sản phẩm nào."
            EmptyDataRowStyle-CssClass="db-empty"
            OnSelectedIndexChanged="gvData_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Chọn" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                <asp:BoundField DataField="MaSP" HeaderText="Mã sản phẩm" />
                <asp:BoundField DataField="TenSP" HeaderText="Tên sản phẩm" />
                <asp:BoundField DataField="TenLoai" HeaderText="Danh mục" />
                <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0} đ" />
                <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                <asp:BoundField DataField="KichThuoc" HeaderText="Kích thước" />
                <asp:BoundField DataField="MauSac" HeaderText="Màu sắc" />
                <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
            </Columns>
        </asp:GridView>
    </div>
    
    <div class="table-foot mt-3 text-muted">Dữ liệu trong bảng được đọc trực tiếp từ SQL Server database QLCHTT.</div>
</div>
</asp:Content>