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

        public User GetUser(string email, string password) {
             return _context.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
        }
    }
}
