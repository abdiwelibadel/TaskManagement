<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="TaskManagement.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container p-md-4 p-sm-4">
        <asp:Label ID="lblInbox" runat="server" Text="" CssClass="" />
        <h2>Inbox</h2>
        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <asp:GridView ID="GridViewMessages" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" 
                    OnRowDeleting="GridViewMessages_RowDeleting" DataKeyNames="MessageID" EmptyDataText="Empty Inbox">
                    <Columns>
                        <asp:BoundField DataField="MessageID" HeaderText="Message ID" />
                        <asp:BoundField DataField="SenderName" HeaderText="Sender" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Body" HeaderText="Body" />
                        <asp:BoundField DataField="SentDate" HeaderText="Sent Date" DataFormatString="{0:MM/dd/yyyy HH:mm:ss}" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblIsRead" runat="server" Text='<%# Convert.ToBoolean(Eval("IsRead")) ? "Read" : "Unread" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnMarkAsRead" runat="server" Text="Mark as Read" CommandArgument='<%# Eval("MessageID") %>' OnClick="MarkAsRead_Click" CssClass="btn btn-sm btn-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField CausesValidation="False" ShowDeleteButton="True" HeaderText="Operation">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="container p-md-4 p-sm-4">
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="" />
        <h2 class="mt-4">Send a Message</h2>
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
                <asp:Label ID="lblBody" runat="server" Text="Message:" AssociatedControlID="txtBody" />
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
