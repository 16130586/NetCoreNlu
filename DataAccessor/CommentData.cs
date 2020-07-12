using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class CommentData : DataProvider
    {
        public CommentData(OurDbContext context) : base(context)
        {
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
