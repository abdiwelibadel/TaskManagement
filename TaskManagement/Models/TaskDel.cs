using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace TaskManagement.Models
{
    public class TaskDel
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
        // Get all tasks method
        public List<TaskModel> GetTasks()
        {
            List <TaskModel> tasks = new List<TaskModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"Select * from Tasks";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    TaskModel model = new TaskModel()
                    {
                        TaskId = Convert.ToInt32(reader["TaskId"]),
                        Title = reader["Title"].ToString(),
                        DueDate = (DateTime)reader["DueDate"],
                        Priority = reader["Priority"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                    tasks.Add(model);
                }
                return tasks;
            }
        }
        // Add Tasks Method
        public int AddTasks(TaskModel task)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"Insert Into Tasks(Title,DueDate,Priority,Status)Values(@Title,@DueDate,@Priority,@Status)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@DueDate", task.DueDate);
                cmd.Parameters.AddWithValue("@Priority", task.Priority);
                cmd.Parameters.AddWithValue("@Status", task.Status);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        // Update Tasks Method
        public void UpdateTasks(TaskModel task)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = @"Update Tasks Set Title=@Title,Priority=@Priority,Status=@Status Where TaskId=@TaskId";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.Parameters.AddWithValue("@TaskId", task.TaskId);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Priority", task.Priority);
                cmd.Parameters.AddWithValue("@Status", task.Status);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // Delete Method
        public void DeleteTask(int taskID)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Delete From Tasks Where TaskId=@TaskId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@TaskId", taskID);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}