<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="TaskManagement.Notifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container p-md-4 p-sm-4">
    <h2>Notifications</h2>
    <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
        <div class="col-md-6">
            <asp:GridView ID="GridViewNotifications" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField="NotificationID" HeaderText="ID" Visible="false" />
                        <asp:BoundField DataField="Message" HeaderText="Message" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblIsRead" runat="server" Text='<%# Convert.ToBoolean(Eval("IsRead")) ? "Read" : "Unread" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnMarkAsRead" runat="server" Text="Mark as Read" CommandArgument='<%# Eval("NotificationID") %>' OnClick="MarkAsRead_Click" CssClass="btn btn-sm btn-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
</asp:Content>
