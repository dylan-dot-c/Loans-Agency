<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Loans_Agency.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-3 bg-dark text-light p-4">
            
            <asp:Label ID="Label1" CssClass="text-light" runat="server" Text="Label"></asp:Label>
            <asp:Image ID="img_URL" CssClass="img-fluid rounded-circle w-100" runat="server" />
            
            <div>
                <asp:Label ID="lblSideBar" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

    <div class="col-9 p-4">
        <h1>Dashboard</h1>
   
        <div class="row">
        
            <div class="col">
                <h2>Customers</h2>
                <a href="#activeLoans"><span class="btn btn-primary">Active Loans</span></a>
                <a href="#overDueLoans"><span class="btn btn-danger">OverDue Loans</span></a>
                <a href="#paidLoans"><span class="btn btn-success">Paid Loans</span></a>
            </div>
            <div class="col">
                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="+ Add New Customer" data-bs-toggle="modal" data-bs-target="#exampleModal" OnClientClick="return false;" />
            </div>
        </div>
    
    <hr />

    <h2 class="text-primary" id="activeLoans">Active Loans</h2>
    <hr />
    <asp:Label ID="lblActive" runat="server" Text=""></asp:Label>

    <h2 class="text-danger" id="overDueLoans">OverDue Loans</h2>
      <hr />
    <asp:Label ID="lblOverDue"  runat="server" Text=""></asp:Label>

        <h2 class="text-success" id="paidLoans">Paid Loans</h2>
      <hr />
    <asp:Label ID="lblPaid" runat="server" Text=""></asp:Label>
    </div>
    </div>

    <asp:Repeater ID="rptContent" runat="server">
    <ItemTemplate>
        <div class="mb-3 border-4 text-bg-dark p-4 "><%# Eval("first_name") %> <%# Eval("last_name") %> <%# Eval("request_date") %> <%# Eval("principal") %> <%# Eval("due_amount") %>

        <asp:HyperLink NavigateUrl='<%#  "~/LoanDetails.aspx?id=" + Eval("Id") %>'  runat="server" CssClass="btn-danger btn">See Details</asp:HyperLink>
        </div>
    </ItemTemplate>
</asp:Repeater>

    
<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        
          <div class="">

              <div class="row">
                  <div class="col">
                  <label class="form-label">First Name </label>
                    <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>

              <div class="col">
                  <label class="form-label">Last Name </label>
                  <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                </div>
              
              <div class="row">
                   <div class="col">
              <label class="form-label">TRN </label>
                <asp:TextBox ID="txtTRN" CssClass="form-control" runat="server"></asp:TextBox>
          </div>
                   <div class="col">
              <label class="form-label">Date Of Birth </label>
                <asp:TextBox ID="txtDateBirth" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
          </div>

              </div>

              <div class="row">
                  <div class="col">
              <label class="form-label">Pricipal </label>
                <asp:TextBox ID="txtPrincipal" CssClass="form-control" runat="server"></asp:TextBox>
          </div>

                   <div class="col">
              <label class="form-label">Request Date </label>
                <asp:TextBox ID="txtDateStart" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
          </div>

                  <div class="col">
              <label class="form-label">Due Date </label>
                <asp:TextBox ID="txtDateDue" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
          </div>
                 
              </div>
          </div>


      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <asp:Button ID="btnNewCustomer" CssClass="btn btn-primary" runat="server" Text="Add Customer" OnClick="btnNewCustomer_Click"  />
      </div>
    </div>
  </div>
</div>
   </asp:Content>