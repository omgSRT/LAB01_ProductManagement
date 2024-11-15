﻿using BusinessObject;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product) => ProductDAO.Instance.Add(product);

        public void Delete(Product product) => ProductDAO.Instance.Delete(product);

        public IEnumerable<Product> GetAll() => ProductDAO.Instance.GetAll();

        public IEnumerable<Product> GetAllWith2Include(string field1, string field2) => ProductDAO.Instance.GetAllWith2Include(field1, field2);

        public IEnumerable<Product> GetAllWithInclude(string field) => ProductDAO.Instance.GetAllWithInclude(field);

        public IEnumerable<Product> GetAllWithInclude(int? pageIndex, int? pageSize, string include)
        {
            if (pageIndex == null || (pageIndex != null && pageIndex < 1))
            {
                pageIndex = 1;
            }
            if (pageIndex == null || (pageIndex != null && pageIndex < 1))
            {
                pageIndex = 1;
            }

            var skip = (pageIndex - 1) * pageSize;

            return ProductDAO.Instance.GetAllWithInclude(include)
                .Skip((int)skip!)
                .Take((int)pageSize!)
                .ToList();
        }

        public Product? GetById(int id) => ProductDAO.Instance.GetById(id);

        public void Save() => ProductDAO.Instance.Save();

        public void Update(Product product) => ProductDAO.Instance.Update(product);
    }
}
