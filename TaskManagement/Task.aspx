<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Task.aspx.cs" Inherits="TaskManagement.Task" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div class="container p-md-4 p-sm-4">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <h3 class="text-center">Add New Task</h3>
        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
              <div class="col-md-6">
                  <asp:Label for="txtTitle" runat="server" Text="Title"></asp:Label>
                  <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Task Title" />
                </div>
            <div class="col-md-6">
                <asp:Label for="txtDueDate" runat="server" Text="Due Date"></asp:Label>
                <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" TextMode="Date" Placeholder="Due Date (MM/dd/yyyy)" />
            </div>
         </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <asp:Label for="ddlPriority" runat="server" Text="Priority"></asp:Label>
                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Priority" Value="0" />
                    <asp:ListItem Text="High" Value="High" />
                    <asp:ListItem Text="Medium" Value="Medium" />
                    <asp:ListItem Text="Low" Value="Low" />
                </asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:Label for="ddlStatus" runat="server" Text="Status"></asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Status" Value="0" />
                    <asp:ListItem Text="Pending" Value="Pending" />
                    <asp:ListItem Text="In Progress" Value="In Progress" />
                    <asp:ListItem Text="Completed" Value="Completed" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5">
            <div class="col-md-3 col-md-offset-2 mb-2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Task" CssClass="btn btn-primary btn-block" BackColor="#55588C9" OnClick="btnAdd_Click" />
            </div>
        </div>
        <div class="row mb-3 mr-lg-5 ml-lg-5">
            <div class="col-md-8">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowPaging="true" DataKeyNames="TaskId" AutoGenerateColumns="false" EmptyDataText="No Tasks Available"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="TaskId" HeaderText="Task Id" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" ReadOnly="True" DataFormatString="{0:MM/dd/yyyy HH:mm:ss}">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextTitle" runat="server" CssClass="form-control" Text='<%# Eval("Title") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Priority">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextPriority" runat="server" CssClass="form-control" Text='<%# Eval("Priority") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextStatus" runat="server" CssClass="form-control" Text='<%# Eval("Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField CausesValidation="false" ShowDeleteButton="True" ShowEditButton="True" HeaderText="Operation">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        </div>
        </div>
</asp:Content>
