using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseDAO<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseDAO()
        {
            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }
        public IEnumerable<T> GetAllWithInclude(string field)
        {
            return _dbSet.Include(field);
        }
        public IEnumerable<T> GetAllWith2Include(string field1, string field2)
        {
            return _dbSet.Include(field1).Include(field2);
        }
        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
