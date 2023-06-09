<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanDetails.aspx.cs" Inherits="Loans_Agency.LoanDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        
    <h1>
        <asp:Label ID="lblheader" runat="server" Text=""></asp:Label>
    </h1>
        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="+ Add Payment Information" data-bs-toggle="modal" data-bs-target="#exampleModal" OnClientClick="return false;" />
    <div>
        <asp:Label ID="lblinfo" CssClass="jumbotron" runat="server" Text="Label"></asp:Label>
        <asp:Repeater ID="rptContent" runat="server">
            <ItemTemplate>
                <div>
                    <table class="table table-dark table-striped table-hover table">
                        <tr>
                            <td>First Name</td>
                            <td><%# Eval("first_name") %></td>
                        </tr>
                       <tr>
                            <td>Last Name</td>
                            <td><%# Eval("last_name") %></td>
                        </tr>
                        <tr>
                            <td>Principal</td>
                            <td><%# Eval("principal") %></td>
                        </tr>
                        <tr>
                            <td>Due Date</td>
                            <td><%# Eval("due_date") %></td>
                        </tr>
                        <tr>
                             <td>Amount Paid</td>
                            <td><%# Eval("amount_paid") %></td>
                        </tr>
                            
                            <tr>
                                <td>Last Paid Date</td>
                            <td><%# Eval("last_paid_date") %></td>
                        </tr>
                    </table>
                    
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    </div>

<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Add Payment Info</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <label>Enter AMount</label>
          <asp:TextBox ID="txtPayment" TextMode="Number" runat="server"></asp:TextBox>
          <asp:TextBox ID="txtDatePaid" max='<%# DateTime.Now.ToShortDateString() %>' TextMode="Date" runat="server"></asp:TextBox>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <asp:Button ID="btnAddPayment" OnClick="btnAddPayment_Click" runat="server" Text="Add Payment" CssClass="btn btn-primary" />
      </div>
    </div>
  </div>
</div>

</asp:Content>