<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="TaskManagement.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="container p-md-4 p-sm-4">
      <h2>Send a Message</h2>
      <asp:Label ID="lblMessage" runat="server" Text="" CssClass="" />
      <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
          <div class="col-md-6">
              <asp:Label ID="lblReceiver" runat="server" Text="To:" AssociatedControlID="ddlReceivers" />
              <asp:DropDownList ID="ddlReceivers" runat="server" CssClass="form-control"></asp:DropDownList>
          </div>
      </div>
      <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
          <div class="col-md-6">
              <asp:Label ID="lblSubject" runat="server" Text="Subject:" AssociatedControlID="txtSubject" />
              <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
      </div>
      <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
          <div class="col-md-6">
              <asp:Label ID="lblBody" runat="server" Text="Report:" AssociatedControlID="txtBody" />
              <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
          </div>
           <div class="row mt-3 mr-lg-5 ml-lg-5">
               <div class="col-md-3 col-md-offset-2 mb-2">
                   <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary btn-block" BackColor="#55588C9" OnClick="btnSend_Click" />
               </div>
           </div>
  </div>
</div>
</asp:Content>
