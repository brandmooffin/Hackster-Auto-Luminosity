using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoLuminosityService.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public string ExecuteTime { get; set; }

        public int Action { get; set; }

        public int Type { get; set; }

        public int EntityId { get; set; }

        public string EntityName { get; set; }
    }
}