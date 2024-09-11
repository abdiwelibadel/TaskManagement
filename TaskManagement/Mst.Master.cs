using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace TaskManagement
{
    public partial class Mst : System.Web.UI.MasterPage
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateNotificationCount();
            }
        }
        private int GetUserById(string username)
        {
            int Id = 0;
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("Select UserID, UserName from Users where UserName=@UserName", con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    object result = cmd.ExecuteScalar();
                    if(result != null)
                    {
                        Id = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("User not found!");
                    }
                }
            }
            return Id;
        }
        private string GetCurrentUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }
        private void UpdateNotificationCount()
        {
            try
            {
                // Assuming the username is stored in the session upon login
                string username = GetCurrentUser();

                if (!string.IsNullOrEmpty(username))
                {
                    // Get the current user's ID using the method from the Global class

                    int currentUserId = GetUserById(username);

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
    }
}