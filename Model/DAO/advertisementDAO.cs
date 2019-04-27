using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class advertisementDAO
    {
        TmdtDbContext db = null;
        public advertisementDAO()
        {
            db = new TmdtDbContext();
        }
    }
}
