using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.ConnectionFactory.Interface;
using We._Project.ConnectionFactory.UnitOfWork.Interface;
using We._Project.Repository;
using We._Project.Repository.Interface;

namespace We._Project.ConnectionFactory.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IColaboradorRepository _repository;
        public IDBConnector dbConnector { get; }

        public UnitOfWork(IDBConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }


        public IColaboradorRepository ColaboradorRepository => _repository ?? 
            (_repository = new ColaboradorRepository(dbConnector));


        public void BeginTransaction()
        {
            dbConnector.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        public void CommitTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Rollback();
            }
        }
    }
}
