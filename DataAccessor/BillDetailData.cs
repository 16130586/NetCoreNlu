using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class BillDetailData : DataProvider
    {
        public BillDetailData(OurDbContext context) : base(context)
        {
        }
        public void AddBillDetail(BillDetail billDetail)
        {
            _context.BillDetails.Add(billDetail);
            _context.SaveChanges();
        }
    }
}
