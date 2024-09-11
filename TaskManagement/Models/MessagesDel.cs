using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
namespace TaskManagement.Models
{
    public class MessagesDel
    {
        private readonly string _connectioString = ConfigurationManager.ConnectionStrings["TaskDb"].ConnectionString;
        // Get Messages Method
        public List<MessagesModel> GetMessages()
        {
            List<MessagesModel> messages = new List<MessagesModel>();   
            using (SqlConnection con = new SqlConnection(_connectioString))
            {
                string query = @"Select * from Messages";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    MessagesModel messagesModel = new MessagesModel()
                    {
                        MessageID = Convert.ToInt32(reader["MessageID"]),
                        SenderID = Convert.ToInt32(reader["SenderID"]),
                        ReceiverID = Convert.ToInt32(reader["ReceiverID"]),
                        Subject = reader["Subject"].ToString(),
                        Body = reader["Body"].ToString(),
                        SentDate = (DateTime)reader["SentDate"],
                        IsRead = reader["IsRead"].ToString()
                    };
                    messages.Add(messagesModel);
                }
                return messages;
            }
        }
        // Delete Method
        public void DeleteMessage(int messageId)
        {
            using(SqlConnection con = new SqlConnection(_connectioString))
            {
                SqlCommand cmd = new SqlCommand("Delete From Messages Where MessageID=@MessageID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@MessageID", messageId);

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}