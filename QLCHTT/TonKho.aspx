<%@ Page Title="Quản lý tồn kho" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TonKho.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.TonKho" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mb-4">Quản Lý Tồn Kho</h2>
        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-info d-block" Visible="false" />

        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtTuKhoa" runat="server" CssClass="form-control" placeholder="Tìm theo mã hoặc tên sản phẩm..." />
                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="btn btn-secondary" OnClick="btnTimKiem_Click" CausesValidation="false" />
                    <asp:Button ID="btnLamMoi" runat="server" Text="Làm mới" CssClass="btn btn-outline-secondary" OnClick="btnLamMoi_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvData" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="True">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>