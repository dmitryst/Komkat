using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HEPDataLayer.Repository
{
    public class GenericRepository<T> where T: class
    {
        /// <summary>
        /// Контекст данных
        /// </summary>
        protected HEPedmDatabaseEntities _context;

        /// <summary>
        /// Набор типизированных сущностей
        /// </summary>
        protected DbSet<T> _dbSet;

        public GenericRepository(HEPedmDatabaseEntities context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Создать новый объект
        /// </summary>
        /// <returns></returns>
        public virtual T Create()
        {
            T entity = _dbSet.Create<T>();
            return entity;
        }
        
        /// <summary>
        /// Добавить новый обьект в контекст данных
        /// </summary>
        /// <param name="entity">Объект набора</param>
        public virtual void Add(T entity)
        {
            if (_context.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
                _context.Entry<T>(entity).State = EntityState.Added;
            }
            else
                _dbSet.Add(entity);
        }
    }
}
