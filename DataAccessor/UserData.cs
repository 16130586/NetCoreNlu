using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class UserData : DataProvider
    {
        public UserData(OurDbContext context) : base(context)
        {
        }

        public void AddUser(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
