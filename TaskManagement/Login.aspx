<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TaskManagement.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container mt-5">
        
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="lblMessage" runat="server" CssClass="alert"></asp:Label>
            <div class="card">
                <div class="card-body text-center">
                    <h1 class="card-title mb-4" style="font-size: 30px">Login</h1>
                    <div class="mb-3 d-flex justify-content-center">
                        <asp:Label ID="Label1" runat="server" Text="Username" CssClass="form-label"></asp:Label>
                    </div>
                    <div class="mb-3 d-flex justify-content-center">
                        <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" CssClass="form-control w-75"></asp:TextBox>
                    </div>
                    <div class="mb-3 d-flex justify-content-center">
                        <asp:Label ID="Label2" runat="server" Text="Password" CssClass="form-label"></asp:Label>
                    </div>
                    <div class="mb-3 d-flex justify-content-center">
                        <asp:TextBox type="password" ID="txtPassword" runat="server" PlaceHolder="Password" CssClass="form-control w-75"></asp:TextBox>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-dark btn-block w-75" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
