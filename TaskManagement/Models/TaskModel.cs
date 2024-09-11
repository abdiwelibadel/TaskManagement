using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}