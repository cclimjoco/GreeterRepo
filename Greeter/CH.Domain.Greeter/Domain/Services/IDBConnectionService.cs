using System.Collections.Generic;


namespace CH.Domain.Greeter.DomainServices
{
    public interface IDBConnectionService
    {
        IEnumerable<T> Query<T>(string sql);
        dynamic QueryFirst(string sql);
    }

  

}