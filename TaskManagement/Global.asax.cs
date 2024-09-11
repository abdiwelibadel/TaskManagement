using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TaskManagement
{
    public class Global : System.Web.HttpApplication
    {
        public int GetUserIdByUsername(string username)
        {
            int userId = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UserID FROM Users WHERE UserName = @UserName", connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }

            return userId;
        }

        public List<string> GetAllUsernames()
        {
            List<string> usernames = new List<string>();

            // Replace with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to fetch all usernames from your Users table
                using (SqlCommand command = new SqlCommand("SELECT UserName FROM Users", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            usernames.Add(reader["UserName"].ToString());
                        }
                    }
                }
                connection.Close();
            }

            return usernames;
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
            List<string> allUsernames = GetAllUsernames();
            foreach (var username in allUsernames)
            {
                List<string> roles = roleManager.GetRolesForUser(username);

                foreach (string role in roles)
                {
                    if (!Roles.RoleExists(role))
                    {
                        Roles.CreateRole(role);
                    }

                    if (!Roles.IsUserInRole(username, role))
                    {
                        Roles.AddUserToRole(username, role);
                    }
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}