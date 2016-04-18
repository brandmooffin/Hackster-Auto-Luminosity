using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoLuminosityService.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string CreateDate { get; set; }

        public string ExternalId { get; set; }

        public List<Light> Lights { get; set; }
    }
}