using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManagement.Models;

namespace TaskManagement
{
    
    public partial class Dashboard : System.Web.UI.Page
    {
        Global global = new Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdatePendingTasksMessage();
                UpdateInProgressTasksMessage();
                CompletedProgressTasksMessage();
                UpdateMessageCount();
                UpdateNotificationCount();
            }
        }
        private void UpdatePendingTasksMessage()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE Status = 'Pending'", con);
                con.Open();

                // Use ExecuteScalar to get the result and handle null values
                object result = cmd.ExecuteScalar();

                // Safely cast the result to an int, defaulting to 0 if the result is null
                int pendingTasksCount = result != null ? (int)(result) : 0;

                lblPendingTasksCount.Text = pendingTasksCount.ToString();

                con.Close();
            }
        }
        private void UpdateInProgressTasksMessage()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE Status = 'In Progress'", con);
                con.Open();

                // Use ExecuteScalar to get the result and handle null values
                object result = cmd.ExecuteScalar();

                // Safely cast the result to an int, defaulting to 0 if the result is null
                int inProgressTasksCount = result != null ? (int)(result) : 0;

                lblInProgressTasksCount.Text = inProgressTasksCount.ToString();

                con.Close();
            }
        }
        private void CompletedProgressTasksMessage()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tasks WHERE Status = 'Completed'", con);
                con.Open();

                // Use ExecuteScalar to get the result and handle null values
                object result = cmd.ExecuteScalar();

                // Safely cast the result to an int, defaulting to 0 if the result is null
                int completedTasksCount = result != null ? (int)(result) : 0;

                lblCompletedTasksCount.Text = completedTasksCount.ToString();

                con.Close();
            }
        }
        private void UpdateMessageCount()
        {
            try
            {
                // Assuming the username is stored in the session upon login
                string username = GetCurrentUsername();

                if (!string.IsNullOrEmpty(username))
                {
                    // Get the current user's ID using the method from the Global class
                    
                    int currentUserId = global.GetUserIdByUsername(username);

                    string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Messages WHERE ReceiverID = @ReceiverID AND IsRead = 0", con);
                        cmd.Parameters.AddWithValue("@ReceiverID", currentUserId);

                        con.Open();
                        object result = cmd.ExecuteScalar();
                        int newMessagesCount = result != null ? (int)(result) : 0;
                        con.Close();

                        // Update the placeholder with the dynamic count
                        newMessagesCountLabel.Text = newMessagesCount.ToString();
                    }
                }
                else
                {
                    // Handle the case where the user is not logged in
                    newMessagesCountLabel.Text = "0";
                }
            }
            catch
            {
                // Log the exception or handle it accordingly
                newMessagesCountLabel.Text = "Error";
            }
        }
        private void UpdateNotificationCount()
        {
            try
            {
                // Assuming the username is stored in the session upon login
                string username = GetCurrentUsername();

                if (!string.IsNullOrEmpty(username))
                {
                    // Get the current user's ID using the method from the Global class

                    int currentUserId = global.GetUserIdByUsername(username);

                    string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Notifications WHERE NotificationID = @NotificationID AND IsRead = 0", con);
                        cmd.Parameters.AddWithValue("@NotificationID", currentUserId);

                        con.Open();
                        object result = cmd.ExecuteScalar();
                        int newNotificationCount = result != null ? (int)(result) : 0;
                        con.Close();

                        // Update the placeholder with the dynamic count
                        newNotificationLabel.Text = newNotificationCount.ToString();
                    }
                }
                else
                {
                    // Handle the case where the user is not logged in
                    newNotificationLabel.Text = "0";
                }
            }
            catch
            {
                // Log the exception or handle it accordingly
                throw;
            }
        }

        private string GetCurrentUsername()
        {
            // Implement based on your authentication system
            return User.Identity.Name; // ASP.NET Identity example
        }
    }
    
}