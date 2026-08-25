using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using SchoolDatabase.Context;
using BORFinanceRepository.Extensions;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {
        private readonly BORFinanceDbContext _context;

        public StoredProcedureRepository(BORFinanceDbContext context)
        {
            _context = context;
        }

        public async Task<int> ExecuteAsync(string procedureName, params MySqlParameter[] parameters)
        {
            using var connection = _context.Database.GetDbConnection();

            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var parameter in parameters)
                command.Parameters.Add(parameter);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<T>> QueryAsync<T>(string procedureName, params MySqlParameter[] parameters)
            where T : class, new()
        {
            using var connection = _context.Database.GetDbConnection();

            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var parameter in parameters)
                command.Parameters.Add(parameter);

            using var reader = await command.ExecuteReaderAsync();

            List<T> list = new();

            while (await reader.ReadAsync())
            {
                T item = new();

                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (!reader.HasColumn(property.Name))
                        continue;

                    var value = reader[property.Name];

                    if (value == DBNull.Value)
                        continue;

                    property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                }

                list.Add(item);
            }

            return list;
        }
    }
}
