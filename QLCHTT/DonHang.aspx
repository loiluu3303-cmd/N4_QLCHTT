<%@ Page Title="Đơn hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DonHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DonHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-heading">
        <div>
            <h1>Quản lý đơn hàng</h1>
            <p>Theo dõi hóa đơn bán hàng, thanh toán và đổi trả.</p>
        </div>
        <asp:Button ID="btnTaoDonHang" runat="server" Text="+ Tạo đơn hàng" CssClass="btn btn-success" OnClick="btnTaoDonHang_Click" />
    </div>

    <div class="panel-card">
        <div class="toolbar">
            <asp:TextBox ID="txtTimKiem" runat="server" CssClass="form-control search-box" placeholder="Tìm mã hóa đơn hoặc khách hàng..." />
            <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="form-select filter-box">
                <asp:ListItem Text="Tất cả trạng thái" Value="" />
                <asp:ListItem Text="Đã thanh toán" Value="Đã thanh toán" />
                <asp:ListItem Text="Chờ thanh toán" Value="Chờ thanh toán" />
                <asp:ListItem Text="Đã hủy" Value="Đã hủy" />
            </asp:DropDownList>
            <asp:Button ID="btnLoc" runat="server" Text="Lọc" CssClass="btn btn-outline-secondary" OnClick="btnLoc_Click" />
        </div>

        <asp:Label ID="lblThongBao" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />

        <div class="table-responsive">
            <asp:GridView ID="gvData" runat="server" CssClass="table db-grid" GridLines="None"
                AutoGenerateColumns="false" EmptyDataText="Database chưa có dữ liệu cho mục này."
                EmptyDataRowStyle-CssClass="db-empty" DataKeyNames="MaHoaDon" OnRowCommand="gvData_RowCommand">
                <Columns>
                    <asp:ButtonField ButtonType="Link" CommandName="XemChiTiet" DataTextField="MaHoaDon" HeaderText="Mã hóa đơn" />
                    <asp:BoundField DataField="NgayLap" HeaderText="Ngày lập" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="KhachHang" HeaderText="Khách hàng" />
                    <asp:BoundField DataField="NhanVien" HeaderText="Nhân viên" />
                    <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0} ₫" />
                    <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
                    <asp:BoundField DataField="KhuyenMai" HeaderText="Khuyến mãi" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="panel-card mt-3">
        <div class="panel-header">
            <div>
                <h5>Chi tiết hóa đơn</h5>
                <p>Sản phẩm, số lượng và thành tiền theo từng hóa đơn</p>
            </div>
        </div>
        <asp:Label ID="lblThongBaoChiTiet" runat="server" CssClass="db-message alert alert-info d-block" Visible="false" />
        <div class="table-responsive">
            <asp:GridView ID="gvChiTiet" runat="server" CssClass="table db-grid" GridLines="None" 
                AutoGenerateColumns="false" EmptyDataText="Chưa có chi tiết hóa đơn (Bấm vào mã hóa đơn ở bảng trên để xem)." >
                <Columns>
                    <asp:BoundField DataField="MaHoaDon" HeaderText="Mã hóa đơn" />
                    <asp:BoundField DataField="MaSP" HeaderText="Mã sản phẩm" />
                    <asp:BoundField DataField="TenSP" HeaderText="Tên sản phẩm" />
                    <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                    <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0} ₫" />
                    <asp:BoundField DataField="ThanhTien" HeaderText="Thành tiền" DataFormatString="{0:N0} ₫" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
