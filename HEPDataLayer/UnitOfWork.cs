using HEPDataLayer.Repository;
using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEPDataLayer
{
    public class UnitOfWork
    {
        private HEPedmDatabaseEntities _dbContext;
        private DbTransaction _transaction;

        public ItemRepository ItemRepository { get; set; }
        public GenericRepository<Language> LanguageRepository { get; set; }
        public GenericRepository<Country> CountryRepository { get; set; }
        public BrandRepository BrandRepository { get; set; }
        public MachineTypeRepository MachineTypeRepository { get; set; }
        
        public ModelRepository ModelRepository { get; set; }
        public GenericRepository<Category> CategoryRepository { get; set; }
        public GenericRepository<DescriptionText> DescriptionRepository { get; set; }
        public GenericRepository<Currency> CurrencyRepository { get; set; }
        public ItemPriceRepository ItemPriceRepository { get; set; }
        public UnitOfWork()
        {
            _dbContext = new HEPedmDatabaseEntities();

            Init();
        }

        private void Init()
        {
            ItemRepository = new ItemRepository(_dbContext);
            LanguageRepository = new GenericRepository<Language>(_dbContext);
            CountryRepository = new GenericRepository<Country>(_dbContext);
            BrandRepository = new BrandRepository(_dbContext);
            MachineTypeRepository = new MachineTypeRepository(_dbContext);
            
            ModelRepository = new ModelRepository(_dbContext);
            CategoryRepository = new GenericRepository<Category>(_dbContext);
            DescriptionRepository = new GenericRepository<DescriptionText>(_dbContext);
            CurrencyRepository = new GenericRepository<Currency>(_dbContext);
            ItemPriceRepository = new ItemPriceRepository(_dbContext);
        }

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }
            OpenConnection();
            _transaction = ((IObjectContextAdapter)_dbContext).ObjectContext.Connection.BeginTransaction(isolationLevel);
        }

        private void OpenConnection()
        {
            if (((IObjectContextAdapter)_dbContext).ObjectContext.Connection.State != ConnectionState.Open)
                ((IObjectContextAdapter)_dbContext).ObjectContext.Connection.Open();
        }


        public void CommitTransaction()
        {
            if (_transaction == null)
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");

            try
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges();
                _transaction.Commit();
                ReleaseCurrentTransaction();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
        }

        private void ReleaseCurrentTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void RollBackTransaction()
        {
            if (_transaction == null)
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            else
            {
                _transaction.Rollback();
                ReleaseCurrentTransaction();
            }

        }

        /// <summary>
        /// Реализация IDisposable
        /// </summary>
        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_dbContext != null)
                {
                    if (_transaction != null)
                        RollBackTransaction();

                    _dbContext.Dispose();
                    _dbContext = null;
                }
        }

        #endregion // IDisposable
    }
}
