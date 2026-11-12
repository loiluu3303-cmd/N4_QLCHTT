<%@ Page Title="Kiểm kê kho" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KiemKe.aspx.cs" Inherits="QLCHTT_WebForms_Frontend.KiemKe" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h2 class="mb-4">Kiểm Kê Kho Hàng</h2>
        <asp:Label ID="lblThongBao" runat="server" CssClass="alert alert-info d-block" Visible="false" />

        <div class="row mb-3">
            <div class="col-md-5">
                <div class="input-group">
                    <asp:TextBox ID="txtTimKiem" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm kiểm kê..." />
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-secondary" OnClick="btnSearch_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="card p-3">
                    <h4>Phiếu kiểm kê</h4>
                    <div class="mb-2"><label>Sản phẩm</label><asp:DropDownList ID="ddlMaSP" runat="server" CssClass="form-control" /></div>
                    <div class="mb-3"><label>Số lượng thực tế</label><asp:TextBox ID="txtSoLuongThucTe" runat="server" CssClass="form-control" TextMode="Number" /></div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Cập nhật kiểm kê" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn btn-secondary" OnClick="btnReset_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <asp:GridView ID="gvData" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="MaSP" HeaderText="Mã SP" />
                        <asp:BoundField DataField="TenSP" HeaderText="Tên sản phẩm" />
                        <asp:BoundField DataField="TonKho" HeaderText="Tồn hệ thống" />
                        <asp:BoundField DataField="ThucTe" HeaderText="Thực tế" />
                        <asp:BoundField DataField="ChenhLech" HeaderText="Chênh lệch" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>