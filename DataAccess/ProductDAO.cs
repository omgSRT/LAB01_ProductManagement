using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO : BaseDAO<Product>
    {
        private static ProductDAO instance = null!;
        private static object lockObject = new object();
        public ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
    }
}
