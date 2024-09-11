using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services.Description;

namespace TaskManagement
{
    public partial class Report : System.Web.UI.Page
    {
        Global global = new Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReceiver();
            }
        }
        private void BindReceiver()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select UserID, UserName from Users", con);
                ddlReceivers.DataSource = cmd.ExecuteReader();
                ddlReceivers.DataTextField = "UserName";
                ddlReceivers.DataValueField = "UserID";
                ddlReceivers.DataBind();
                ddlReceivers.Items.Insert(0, "Select Receiver");
            }
        }
        private void BindReport()
        {
            try
            {
                string user = CurrentUser();
                if (String.IsNullOrEmpty(user))
                {
                    lblMessage.Text = "User is not logged in";
                    lblMessage.CssClass = "alert alert-danger";
                    return;
                }
                int userId = global.GetUserIdByUsername(user);
                string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select R.ReportId, U.UserName AS SenderName, R.Report, R.SentDate,R.IsRead " +
                                                    "FROM Report R INNER JOIN User U ON R.SenderId=U.UserId Where R.ReceiverId=@ReceiverID", con);
                    cmd.Parameters.AddWithValue("@ReceiverID", userId);

                }

            }
            catch
            {
                throw;
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string username = CurrentUser();
                if (!String.IsNullOrEmpty(username))
                {
                    int SenderId = global.GetUserIdByUsername(username);
                    string Report = txtBody.Text.Trim();
                    string Subject = txtSubject.Text.Trim();
                    int ReceiverId = Convert.ToInt32(ddlReceivers.SelectedValue);
                    string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert Into Reports(SenderID,ReceiverID,Report,SentDate)Values(@SenderID,@ReceiverID,@Report,@SentDate)", con);
                        cmd.Parameters.AddWithValue("@SenderID", SenderId);
                        cmd.Parameters.AddWithValue("@ReceiverID", ReceiverId);
                        cmd.Parameters.AddWithValue("@Report", Report);
                        cmd.Parameters.AddWithValue("@SentDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        InsertNotification(ReceiverId, $"You've received a new message from {username}: {Subject}: {Report}");
                        lblMessage.Text = "Report Sent Successfully";
                        lblMessage.CssClass = "alert alert-success";
                        ClearFields();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void InsertNotification(int userId, string message)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
            using(SqlConnection con = new SqlConnection(_connectionString))
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
        private string CurrentUser()
        {
            return User.Identity.Name;
        }
        private void ClearFields()
        {
            txtBody.Text = String.Empty;
            txtSubject.Text = String.Empty;
            ddlReceivers.SelectedIndex = 0;
        }

    }
}