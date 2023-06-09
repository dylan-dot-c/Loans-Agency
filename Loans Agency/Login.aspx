<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Loans_Agency.About" EnableEventValidation="true"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div>
            <h1>Login</h1>
            <p>Welcome back. Please enter your credentials to LOGIN.</p>
        </div>
        <div>
            <div class="mb-3">
                <label class="form-label">Username:</label>
                <br />
                <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" ClientIDMode="AutoID"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Password:</label>
                <br />
                <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server" ClientIDMode="AutoID"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Login" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
