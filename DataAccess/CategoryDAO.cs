using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO : BaseDAO<Category>
    {
        private static CategoryDAO instance = null!;
        private static object lockObject = new object();
        public CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
    }
}
