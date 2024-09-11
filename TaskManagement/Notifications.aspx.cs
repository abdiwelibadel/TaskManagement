using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManagement
{
    public partial class Notifications : System.Web.UI.Page
    {
        Global global = new Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotifications();
            }
        }
        private void BindNotifications()
        {
            try
            {
                string username = GetCurrentUsername();
                if (!string.IsNullOrEmpty(username))
                {
                    int currentUserId = global.GetUserIdByUsername(username);

                    string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT NotificationID, Message, IsRead, CreatedDate FROM Notifications WHERE UserID = @UserID", con);
                        cmd.Parameters.AddWithValue("@UserID", currentUserId);

                        con.Open();
                        GridViewNotifications.DataSource = cmd.ExecuteReader();
                        GridViewNotifications.DataBind();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void MarkAsRead_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int notificationId = Convert.ToInt32(btn.CommandArgument);

            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID", con);
                cmd.Parameters.AddWithValue("@NotificationID", notificationId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            BindNotifications(); // Refresh the GridView
        }

        private string GetCurrentUsername()
        {
            // Implement based on your authentication system
            return User.Identity.Name; // ASP.NET Identity example
        }
    }
}