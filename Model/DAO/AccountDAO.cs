using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class AccountDAO
    {
        TmdtDbContext db = null;
        private object x;

        public AccountDAO()
        {
            db = new TmdtDbContext();
        }
        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public Account GetInfoByUsername(string username)
        {
            return db.Accounts.SingleOrDefault(x => x.Username == username);
        }
        public Account FindByUsername(string username)
        {
            var row = db.Accounts.Where(x => x.Username.Equals(username));
            return row.SingleOrDefault();
        }
        public bool IsExitsEmail(string email)
        {
            var row = db.Accounts.SingleOrDefault(x => x.Email == email);
            if (row == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsExitsPhone(string phone)
        {
            var row = db.Accounts.SingleOrDefault(x => x.Phone == phone);
            if (row == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public int Login(string userName, string passWord)
        {
            var result = db.Accounts.SingleOrDefault(x => x.Username == userName);

            if (result == null)
            {
                return 0; // account không tồn tại
            }
            else
            {
                if (result.Password == passWord)
                {
                    return 1; //mật khảu chính xác
                }
                else
                {
                    return -2; //sai mật khẩu
                }
            }
        }

        public bool UpdateStatusUser(string username)
        {
            try
            {
                var user = GetInfoByUsername(username);
                user.Status = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<Account> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Account> model = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
    }
}
