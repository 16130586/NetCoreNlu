using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NameUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImgeUser { get; set; }
        public string NumberPhone { get; set; }
        public string Gender { get; set; }
        public string Level { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Active { get; set; }

        public bool CheckLevel()
        {
            return Level.Equals("admin") || Level.Equals("manager") ? true : false;
        }
    }
}
