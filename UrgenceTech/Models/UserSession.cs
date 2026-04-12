using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgenceTech.Models
{
    public class UserSession
    {
        public string Token { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsActive { get; set; }


    }
}
