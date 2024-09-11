using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TaskManagement.Models;

namespace TaskManagement
{
    public partial class Task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindView();
            }
        }
        // Get all the Task
        public void BindView()
        {
            TaskDel taskDel = new TaskDel();
            var task = taskDel.GetTasks();
            GridView1.DataSource = task;
            GridView1.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Title = txtTitle.Text;
                DateTime DueDate = Convert.ToDateTime(txtDueDate.Text);
                string Priority = ddlPriority.Text;
                string Status = ddlStatus.Text;
                TaskDel taskDel = new TaskDel();
                TaskModel task = new TaskModel()
                {
                    Title = Title,
                    DueDate = DueDate,
                    Priority = Priority,
                    Status = Status,
                };
                taskDel.AddTasks(task);
                lblMessage.Text = "Task Added Successfully";
                lblMessage.CssClass = "alert alert-success";
                if (Status == "Pending")
                {
                    UpdatePendingTasksCount();
                }
                ClearInputFields();
                BindView();
            }
            catch
            {
                throw;
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            TaskDel taskDel = new TaskDel();
            taskDel.DeleteTask(Id);
            lblMessage.Text = "Task Deleted Successfully";
            lblMessage.CssClass = "alert alert-danger";
            GridView1.EditIndex = -1;
            BindView();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string Title = (row.FindControl("TextTitle") as TextBox).Text.Trim();
                string Priority = (row.FindControl("TextPriority") as TextBox).Text.Trim();
                string Status = (row.FindControl("TextStatus") as TextBox).Text.Trim();
                TaskDel del = new TaskDel();
                TaskModel task = new TaskModel()
                {
                    TaskId = Id,
                    Title = Title,
                    Priority = Priority,
                    Status = Status,
                };
                del.UpdateTasks(task);
                lblMessage.Text = "Task Updated Successfully";
                lblMessage.CssClass = "alert alert-success";
                if(Status == "Pending")
                {
                    UpdateInProgressTasksCount();
                }
                else if (Status == "In Progress")
                {
                    UpdateCompletedTasksCount();
                }
                GridView1.EditIndex = -1;
                BindView();
            }
            catch
            {
                throw;
            }
        }
        // Clear Input Fields Method
        public void ClearInputFields()
        {
            txtTitle.Text = String.Empty;
            txtDueDate.Text = String.Empty;
            ddlPriority.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        private void UpdatePendingTasksCount()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE DashboardStats SET PendingTasksCount = (SELECT COUNT(*) FROM Tasks WHERE Status = 'Pending')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        private void UpdateInProgressTasksCount()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE DashboardStats SET InProgressTasksCount = (SELECT COUNT(*) FROM Tasks WHERE Status = 'In Progress')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }

        }

        private void UpdateCompletedTasksCount()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE DashboardStats SET CompletedTasksCount = (SELECT COUNT(*) FROM Tasks WHERE Status = 'Completed')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        
        
    }
}   