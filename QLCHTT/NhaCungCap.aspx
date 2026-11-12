<%@ Page Title="Quản lý nhà cung cấp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhaCungCap.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.NhaCungCap" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mb-4">Quản Lý Nhà Cung Cấp</h2>
        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-info d-block" Visible="false" />

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtTimKiem" runat="server" CssClass="form-control" placeholder="Tìm kiếm nhà cung cấp..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-secondary" OnClick="btnSearch_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="card p-3">
                    <h4>Thông tin nhà cung cấp</h4>
                    <div class="mb-2"><label>Mã NCC</label><asp:TextBox ID="txtMaNCC" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Tên nhà cung cấp</label><asp:TextBox ID="txtTenNCC" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Số điện thoại</label><asp:TextBox ID="txtSDT" runat="server" CssClass="form-control" /></div>
                    <div class="mb-2"><label>Email</label><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" /></div>
                    <div class="mb-3"><label>Địa chỉ</label><asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" /></div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn btn-danger" OnClick="btnDelete_Click" CausesValidation="false" />
                        <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn btn-secondary" OnClick="btnReset_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <asp:GridView ID="gvData" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnSelectedIndexChanged="gvData_SelectedIndexChanged" DataKeyNames="MaNCC">
                    <Columns>
                        <asp:BoundField DataField="MaNCC" HeaderText="Mã NCC" />
                        <asp:BoundField DataField="TenNCC" HeaderText="Tên nhà cung cấp" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Chọn" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>