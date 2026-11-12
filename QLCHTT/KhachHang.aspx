<%@ Page Title="Quản lý khách hàng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KhachHang.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KhachHang" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mb-4">Quản Lý Khách Hàng</h2>
        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-info d-block" />

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Tìm theo tên hoặc số điện thoại..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-secondary" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="card p-3">
                    <h4>Thông tin khách hàng</h4>
                    <div class="mb-2"><label>Mã KH</label><asp:TextBox ID="txtMaKH" runat="server" CssClass="form-control" ReadOnly="true" /></div>
                    <div class="mb-2"><label>Họ tên</label><asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Số điện thoại</label><asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Địa chỉ</label><asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Điểm tích lũy</label><asp:TextBox ID="txtDiemTichLuy" runat="server" CssClass="form-control" Text="0" /></div>
                    <div class="mb-3"><label>Hạng thành viên</label><asp:TextBox ID="txtHangThanhVien" runat="server" CssClass="form-control" ReadOnly="true" Text="Thành viên mới" /></div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn btn-danger" OnClick="btnDelete_Click" CausesValidation="false" />
                        <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn btn-secondary" OnClick="btnReset_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <asp:GridView ID="gvData" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnSelectedIndexChanged="gvData_SelectedIndexChanged" DataKeyNames="MaKH">
                    <Columns>
                        <asp:BoundField DataField="MaKH" HeaderText="Mã KH" />
                        <asp:BoundField DataField="HoTen" HeaderText="Họ tên" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="DiemTichLuy" HeaderText="Điểm" />
                        <asp:BoundField DataField="HangThanhVien" HeaderText="Hạng" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Chọn" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>