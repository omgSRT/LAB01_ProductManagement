using BusinessObject;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category category) => CategoryDAO.Instance.Add(category);

        public void Delete(Category category) => CategoryDAO.Instance.Delete(category);

        public IEnumerable<Category> GetAll() => CategoryDAO.Instance.GetAll();

        public IEnumerable<Category> GetAll(int? pageIndex, int? pageSize)
        {
            if(pageIndex == null || (pageIndex != null && pageIndex < 1))
            {
                pageIndex = 1;
            }
            if (pageIndex == null || (pageIndex != null && pageIndex < 1))
            {
                pageIndex = 1;
            }

            var skip = (pageIndex - 1) * pageSize;

            return CategoryDAO.Instance.GetAll()
                .Skip((int)skip!)
                .Take((int)pageSize!)
                .ToList();
        }

        public IEnumerable<Category> GetAllWith2Include(string field1, string field2) => CategoryDAO.Instance.GetAllWith2Include(field1, field2);

        public IEnumerable<Category> GetAllWithInclude(string field) => CategoryDAO.Instance.GetAllWithInclude(field);
        public Category? GetById(int id) => CategoryDAO.Instance.GetById(id);

        public void Save() => CategoryDAO.Instance.Save();

        public void Update(Category category) => CategoryDAO.Instance.Update(category);
    }
}
