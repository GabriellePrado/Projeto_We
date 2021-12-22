using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using We._Project.ConnectionFactory.Interface;
using We._Project.Repository.Interface;

namespace We._Project.ConnectionFactory.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IColaboradorRepository ColaboradorRepository { get; }
        IDBConnector dbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
