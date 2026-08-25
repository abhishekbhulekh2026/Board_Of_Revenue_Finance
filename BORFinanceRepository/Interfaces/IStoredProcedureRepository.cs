using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IStoredProcedureRepository
    {
        Task<List<T>> QueryAsync<T>(string procedureName, params MySqlParameter[] parameters)
            where T : class, new();

        Task<int> ExecuteAsync(string procedureName, params MySqlParameter[] parameters);
    }
}
