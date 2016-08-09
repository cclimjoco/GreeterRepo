
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

using Dapper;

namespace CH.Domain.Greeter.DomainServices
{
    [ExcludeFromCodeCoverage]
    public class DBConnectionServiceAgent : IDBConnectionService
    {
        private readonly IDbConnection _dbConnection;

        public DBConnectionServiceAgent(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
           // _dbConnection.QueryFirst
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            return _dbConnection.Query<T>(sql);
        }

        public dynamic QueryFirst(string sql)
        {
            return _dbConnection.QueryFirst(sql);
        } 



    }
}
