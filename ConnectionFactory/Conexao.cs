using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace We._Project.ConnectionFactory
{
    public class Conexao
    {
        public IDbConnection _dbConnection { get; private set; }

        public Conexao(IDbConnection DbConnection)
        {

        }
    }
}
