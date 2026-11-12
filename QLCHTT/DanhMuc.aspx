<%@ Page Title="Quản lý danh mục" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanhMuc.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.DanhMuc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mb-4">Quản Lý Danh Mục Sản Phẩm</h2>
        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-info d-block" Visible="false" />

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Tìm kiếm danh mục..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-secondary" OnClick="btnSearch_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-5">
                <div class="card p-3">
                    <h4>Thông tin danh mục</h4>
                    <div class="mb-2">
                        <label>Mã loại</label>
                        <asp:TextBox ID="txtMaLoai" runat="server" CssClass="form-control" placeholder="Ví dụ: L01" />
                    </div>
                    <div class="mb-2">
                        <label>Tên loại sản phẩm</label>
                        <asp:TextBox ID="txtTenLoai" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Mô tả</label>
                        <asp:TextBox ID="txtMoTa" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                    </div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn btn-danger" OnClick="btnDelete_Click" CausesValidation="false" />
                        <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn btn-secondary" OnClick="btnReset_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <asp:GridView ID="gvData" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" OnSelectedIndexChanged="gvData_SelectedIndexChanged" DataKeyNames="MaLoai">
                    <Columns>
                        <asp:BoundField DataField="MaLoai" HeaderText="Mã loại" />
                        <asp:BoundField DataField="TenLoai" HeaderText="Tên loại sản phẩm" />
                        <asp:BoundField DataField="MoTa" HeaderText="Mô tả" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Chọn" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>