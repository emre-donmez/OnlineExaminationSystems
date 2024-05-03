using Dapper;
using OnlineExaminationSystems.API.Data.Context;
using OnlineExaminationSystems.API.Model.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace OnlineExaminationSystems.API.Model.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity
    {
        private readonly DapperContext _context;
        private readonly string _tableName;
        private readonly List<string> _columnNames;
        private readonly List<string> _columnNamesForValue;
        private readonly List<string> _columnNamesForSelection;

        public GenericRepository(DapperContext context)
        {
            _context = context;
            _tableName = GetTableName();
            _columnNames = GetColumnNames(false);
            _columnNamesForValue = GetColumnNamesForValue();
            _columnNamesForSelection = GetColumnNames(true);
        }

        private static string GetTableName()
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            return tableAttribute == null ? type.Name : tableAttribute.Name;
        }

        private static List<string> GetColumnNamesForValue()
        {
            return typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
        }

        private static List<string> GetColumnNames(bool includeAlias = false)
        {
            var columnNames = new List<string>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.Name == "Id" && !includeAlias) continue;

                var columnName = property.Name.ToLowerInvariant();
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                if (columnAttribute != null && includeAlias)
                    columnName = $"{columnAttribute.Name} As {columnName}";
                else if (columnAttribute != null)
                    columnName = columnAttribute.Name;

                columnNames.Add(columnName);
            }

            return columnNames;
        }

        private IEnumerable<string> SetValues()
        {
            var columnNames = new List<string>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.Name == "Id") continue;

                var columnName = property.Name;
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                if (columnAttribute != null)
                    columnName = $"{columnAttribute.Name} =@{columnName}";
                else
                    columnName = $"{columnName} =@{columnName}";

                columnNames.Add(columnName);
            }

            return columnNames;

        }

        public T Insert(T model)
        {
            var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNames)}) VALUES (@{string.Join(", @", _columnNamesForValue)})" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = _context.CreateConnection())
            {
                var id = connection.QuerySingle<int>(query, model);
                model.Id = id;
            }

            return model;
        }

        public IEnumerable<T> GetAll()
        {
            var query = $"SELECT {string.Join(',',_columnNamesForSelection)} FROM {_tableName} WHERE is_del = 0";

            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<T>(query);

                return result;
            }
        }

        public T? GetById(int id)
        {
            var query = $"SELECT {string.Join(',', _columnNamesForSelection)} FROM {_tableName} WHERE id = {id} AND is_del = 0";

            using (var connection = _context.CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<T>(query);

                return result;
            }
        }

        public bool SoftDelete(int id)
        {
            var query = $"UPDATE {_tableName} SET is_del = 1 WHERE id = {id}";

            using (var connection = _context.CreateConnection())
            {
                var result = connection.Execute(query);

                return result > 0 ? true : false;
            }
        }

        public T Update(T model)
        {
            IEnumerable<string> setValues = SetValues();

            var query = $"UPDATE {_tableName} SET {string.Join(", ", setValues)} WHERE id = @Id AND is_del = 0";

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, model);
            }

            return model;
        }

        public IEnumerable<object> ExecuteQuery(string query, object parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query(query, parameters);

                return result;
            }
        }
    }
}