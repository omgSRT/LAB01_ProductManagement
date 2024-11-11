using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllWithInclude(string field);
        IEnumerable<Product> GetAllWithInclude(int? pageIndex, int? pageSize, string include);
        IEnumerable<Product> GetAllWith2Include(string field1, string field2);
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}
