using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace We._Project.ConnectionFactory.Interface
{
    public interface IDBConnector
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }

        IDbTransaction BeginTransaction(IsolationLevel isolation);
    }
}
