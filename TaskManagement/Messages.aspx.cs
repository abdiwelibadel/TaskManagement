using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManagement.Models;

namespace TaskManagement
{
    public partial class Messages : System.Web.UI.Page
    {
        // Create an instance of the Global class
        Global global = new Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindReceivers();
                BindMessages();
            }
        }
        private void BindReceivers()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM Users", con);
                con.Open();
                ddlReceivers.DataSource = cmd.ExecuteReader();
                ddlReceivers.DataTextField = "UserName";
                ddlReceivers.DataValueField = "UserID";
                ddlReceivers.DataBind();
                con.Close();
                ddlReceivers.Items.Insert(0, "Select Receiver");
            }
        }
        private void BindMessages()
        {
            try
            {
                // Get the current username
                string currentUsername = GetCurrentUsername();

                if (string.IsNullOrEmpty(currentUsername))
                {
                    lblMessage.Text = "Current username is empty.";
                    lblMessage.CssClass = "alert alert-danger";
                    return;
                }

                // Get the current user's ID
                int currentUserId = global.GetUserIdByUsername(currentUsername);

                string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT M.MessageID, U.UserName AS SenderName, M.Subject, M.Body, M.SentDate, M.IsRead FROM Messages M INNER JOIN Users U ON M.SenderID = U.UserID WHERE M.ReceiverID = @ReceiverID", con);
                    cmd.Parameters.AddWithValue("@ReceiverID", currentUserId);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        GridViewMessages.DataSource = reader;
                        GridViewMessages.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "No messages found.";
                        lblMessage.CssClass = "alert alert-warning";
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                // Optionally log the exception
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current username using the GetCurrentUsername method
                string username = GetCurrentUsername();

                if (!string.IsNullOrEmpty(username))
                {
                    // Get the current user's ID
                    int senderId = global.GetUserIdByUsername(username);

                    // Continue with the rest of the process
                    string subject = txtSubject.Text.Trim();
                    string body = txtBody.Text.Trim();
                    int receiverId = Convert.ToInt32(ddlReceivers.SelectedValue);

                    string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Messages (SenderID, ReceiverID, Subject, Body, SentDate) VALUES (@SenderID, @ReceiverID, @Subject, @Body, @SentDate)", con);
                        cmd.Parameters.AddWithValue("@SenderID", senderId);
                        cmd.Parameters.AddWithValue("@ReceiverID", receiverId);
                        cmd.Parameters.AddWithValue("@Subject", subject);
                        cmd.Parameters.AddWithValue("@Body", body);
                        cmd.Parameters.AddWithValue("@SentDate", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Insert notification for the receiver
                        InsertNotification(receiverId, $"You have received a new message from {username}: {subject}");

                        lblMessage.Text = "Message sent successfully!";
                        lblMessage.CssClass = "alert alert-success";
                        ClearForm();
                        BindMessages();
                    }
                }
                else
                {
                    lblMessage.Text = "User is not logged in.";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch
            {
                lblMessage.Text = "Error: ";
                lblMessage.CssClass = "alert alert-danger";
                // Optionally log the exception
                // LogException(ex); // Implement logging as needed
            }
        }

        // Method to insert a notification
        private void InsertNotification(int userId, string message)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Notifications (UserID, Message, IsRead, CreatedDate) VALUES (@UserID, @Message, 0, @CreatedDate)", con);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Message", message);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        protected void MarkAsRead_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int messageId = Convert.ToInt32(btn.CommandArgument);

            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Messages SET IsRead = 1 WHERE MessageID = @MessageID", con);
                cmd.Parameters.AddWithValue("@MessageID", messageId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            BindMessages(); // Refresh the GridView
        }


        private string GetCurrentUsername()
        {
            return User.Identity.Name; // ASP.NET Identity example
        }

        private void ClearForm()
        {
            txtSubject.Text = "";
            txtBody.Text = "";
            ddlReceivers.SelectedIndex = 0;
        }

        protected void GridViewMessages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(GridViewMessages.DataKeys[e.RowIndex].Values[0]);
                MessagesDel messages = new MessagesDel();
                messages.DeleteMessage(id);
                lblInbox.Text = "Message Deleted Successfully";
                lblInbox.CssClass = "alert alert-danger";
                GridViewMessages.EditIndex = -1;
                BindMessages();
            }
            catch
            {
                throw;
            }

        }
    }
}