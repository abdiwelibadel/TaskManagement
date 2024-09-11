<%@ Page Title="" Language="C#" MasterPageFile="~/Mst.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TaskManagement.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mt-5">
        <div class="col-md-6 mb-4">
        <div class="card shadow mb-4 h-100 d-flex flex-column">
            <div class="card-header bg-secondary text-white">
                <h6 class="m-0 font-weight-bold">Tasks</h6>
            </div>
        <div class="card-body">
            <p class="card-text">You have <strong><asp:Label ID="lblPendingTasksCount" runat="server"></asp:Label></strong> Pending tasks.</p>
            <p class="card-text">You have <strong><asp:Label ID="lblInProgressTasksCount" runat="server"></asp:Label></strong> In Progress tasks.</p>
            <p class="card-text">You have <strong><asp:Label ID="lblCompletedTasksCount" runat="server"></asp:Label></strong> Completed tasks.</p>
            <a href="Task.aspx" class="btn btn-primary btn-block">View Tasks</a>
        </div>
        </div>
        </div>
        <div class="col-md-6 mb-4">
        <div class="card shadow mb-4 h-100 d-flex flex-column">
            <div class="card-header bg-secondary text-white">
                <h6 class="m-0 font-weight-bold">Messages</h6>
            </div>
        <div class="card-body">
            <p class="card-text"><strong><asp:Label ID="newMessagesCountLabel" runat="server">0</asp:Label></strong> new messages.</p>
            <a href="Messages.aspx" class="btn btn-primary btn-block">View Messages</a>
        </div>
        </div>
        </div>
        </div>
    <div class="row">
        <div class="col-md-6 mb-4">
        <div class="card shadow mb-4 h-100 d-flex flex-column">
            <div class="card-header bg-secondary text-white">
                <h6 class="m-0 font-weight-bold">Notifications</h6>
            </div>
        <div class="card-body">
            <p class="card-text"><strong><asp:Label ID="newNotificationLabel" runat="server">0</asp:Label></strong> new notifications.</p>
            <a href="Notifications.aspx" class="btn btn-primary btn-block">View Notifications</a>
        </div>
        </div>
        </div>
        <div class="col-md-6 mb-4">
        <div class="card shadow mb-4 h-100 d-flex flex-column">
            <div class="card-header bg-secondary text-white">
                <h6 class="m-0 font-weight-bold">Reports</h6>
            </div>
        <div class="card-body">
            <p class="card-text">Make a report.</p>
            <a href="Report.aspx" class="btn btn-primary btn-block">View Report Log</a>
        </div>
        </div>
        </div>
       </div>
</asp:Content>
