using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text.Trim();

            // Replace with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Verify the user credentials
                using (SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username=@Username AND PasswordHash=@Password", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // Assume password is stored as plain text, but it should be hashed

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        // Authenticate and assign roles
                        FormsAuthentication.SetAuthCookie(username, false);

                        // Redirect based on role
                        string[] userRoles = Roles.GetRolesForUser(username);

                        if (userRoles.Length > 0)
                        {
                            if (Roles.IsUserInRole(username, "Admin"))
                            {
                                Response.Redirect("~/Dashboard.aspx");
                            }
                            else if (Roles.IsUserInRole(username, "Manager"))
                            {
                                Response.Redirect("~/Dashboard.aspx");
                            }
                            else if (Roles.IsUserInRole(username, "Developer"))
                            {
                                Response.Redirect("~/Dashboard.aspx");
                            }
                            else if (Roles.IsUserInRole(username, "Client"))
                            {
                                Response.Redirect("~/Dashboard.aspx");
                            }
                        }
                        else
                        {
                            lblMessage.Text = "User role not assigned.";
                            lblMessage.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password.";
                        lblMessage.CssClass = "alert alert-danger w-75";
                    }
                }
            }
        }
    }
}