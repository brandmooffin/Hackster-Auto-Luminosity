using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLuminosityIotApp.Models
{
    public class Light
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string CreateDate { get; set; }

        public bool IsOn { get; set; }

        public string ExternalId { get; set; }
    }
}
