using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class UpdateCateParams
    {
        public int id_cate { get; set; }
        public string name_cate { get; set; }
        public string icon_cate { get; set; }

        public int select_Status { get; set; }
    }
}
