
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;

namespace SchoolManagement.Models
{
    public class RoleManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;

        public List<string> GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT r.RoleName FROM Roles r INNER JOIN UserRoles ur ON r.RoleId = ur.RoleId INNER JOIN Users u ON ur.UserId = u.UserId WHERE u.UserName = @UserName", connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader["RoleName"].ToString());
                        }
                    }
                }
            }

            return roles;
        }
    }
}