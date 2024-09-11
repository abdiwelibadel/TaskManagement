using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class MessagesModel
    {
        public int MessageID { get; set; } 
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public string IsRead { get; set; }
    }
}