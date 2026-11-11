<%@ Page Title="Quản lý khách hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KhachHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KhachHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="page-heading">
    <div>
        <h1>Quản lý khách hàng</h1>
        <p>Quản lý hồ sơ và thông tin thành viên của khách hàng.</p>
    </div>
</div>

<div class="panel-card">
    <!-- Khu vực tìm kiếm -->
    <div class="toolbar mb-3 d-flex gap-2">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control search-box w-auto" Placeholder="Tìm theo mã, tên, SĐT..." />
        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-secondary" Text="Tìm kiếm" OnClick="btnSearch_Click" />
        <asp:Button ID="btnThemmoi" runat="server" CssClass="btn btn-success ms-auto" Text="+ Thêm khách hàng" OnClick="btnRefresh_Click" />
    </div>

    <!-- Khung nhập liệu (Thêm / Sửa) -->
    <div class="row g-3 mb-3">
        <div class="col-12 col-md-4">
            <label class="form-label">Mã khách hàng</label>
            <asp:TextBox ID="txtMaKH" runat="server" CssClass="form-control" Placeholder="KH001" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Họ tên</label>
            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" Placeholder="Nhập họ tên" />
        </div>
        <div class="col-12 col-md-4">
            <label class="form-label">Số điện thoại</label>
            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" Placeholder="Nhập số điện thoại" />
        </div>
        <div class="col-12 col-md-6">
            <label class="form-label">Địa chỉ</label>
            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" Placeholder="Nhập địa chỉ" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Điểm tích lũy</label>
            <asp:TextBox ID="txtDiemTichLuy" runat="server" CssClass="form-control" Text="0" />
        </div>
        <div class="col-12 col-md-3">
            <label class="form-label">Hạng thành viên</label>
            <asp:TextBox ID="txtHangThanhVien" runat="server" CssClass="form-control" Placeholder="Thường / Bạc / Vàng / Kim cương" />
        </div>
    </div>

    <!-- Các nút thao tác -->
    <div class="d-flex gap-2 mb-3">
        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Lưu dữ liệu" OnClick="btnSave_Click" />
        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Cập nhật" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Xóa" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa khách hàng này không?');" />
        <asp:Button ID="btnRefresh" runat="server" CssClass="btn btn-outline-secondary" Text="Làm mới" OnClick="btnRefresh_Click" />
    </div>

    <!-- Thông báo -->
    <asp:Label ID="lblThongBao" runat="server" CssClass="db-message" Visible="false" />

    <!-- Bảng hiển thị dữ liệu -->
    <div class="table-responsive">
        <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
            AutoGenerateColumns="true" EmptyDataText="Database chưa có dữ liệu khách hàng."
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