using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllWithInclude(string field);
        IEnumerable<Category> GetAllWith2Include(string field1, string field2);
        Category? GetById(long id);
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Save();
    }
}
