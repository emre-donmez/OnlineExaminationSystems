using Dapper;
using OnlineExaminationSystems.API.Data.Context;
using OnlineExaminationSystems.API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace OnlineExaminationSystems.API.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity
    {
        private readonly DapperContext _context;
        private readonly string _tableName;
        private readonly List<string> _columnNamesWithAttribute;
        private readonly List<string> _columnNames;
        private readonly List<string> _columnNamesForSelection;
        private readonly List<string> _columnNamesForUpdate;

        public GenericRepository(DapperContext context)
        {
            _context = context;
            _tableName = GetTableName();

            var (columnNamesWithAttribute, columnNames, columnNamesForSelection, columnNamesForUpdate) = GetColumnNamesAll();

            _columnNamesWithAttribute = columnNamesWithAttribute;
            _columnNames = columnNames;
            _columnNamesForSelection = columnNamesForSelection;
            _columnNamesForUpdate = columnNamesForUpdate;
        }

        private (List<string> columnNamesWithAttribute, List<string> columnNames, List<string> selectionColumns, List<string> columnNamesForUpdate) GetColumnNamesAll()
        {
            var columnNamesWithAttribute = new List<string>();
            var columnNames = new List<string>();
            var selectionColumns = new List<string>();
            var columnNamesForUpdate = new List<string>();

            foreach (var property in typeof(T).GetProperties())
            {
                var columnName = property.Name;
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                var selection = columnAttribute != null ? $"{columnAttribute.Name} AS {columnName}" : columnName;

                selectionColumns.Add(selection);

                if (property.Name == "Id") continue;

                columnNames.Add(columnName);

                string value = $"{columnName} =@{columnName}";
                string column = columnName;

                if (columnAttribute != null)
                {
                    value = $"{columnAttribute.Name} =@{columnName}";
                    column = columnAttribute.Name;
                }

                columnNamesWithAttribute.Add(column);
                columnNamesForUpdate.Add(value);
            }

            return (columnNamesWithAttribute, columnNames, selectionColumns, columnNamesForUpdate);
        }

        private static string GetTableName()
        {
            var type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            return tableAttribute == null ? type.Name : tableAttribute.Name;
        }

        public T Insert(T model)
        {
            var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNamesWithAttribute)}) VALUES (@{string.Join(", @", _columnNames)})" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();

            var id = connection.QuerySingle<int>(query, model);
            model.Id = id;

            return model;
        }

        public IEnumerable<T> GetAll()
        {
            var query = $"SELECT {string.Join(',', _columnNamesForSelection)} FROM {_tableName} WHERE is_del = 0";

            using var connection = _context.CreateConnection();
            var result = connection.Query<T>(query);

            return result;
        }

        public T? GetById(int id)
        {
            var query = $"SELECT {string.Join(',', _columnNamesForSelection)} FROM {_tableName} WHERE id = {id} AND is_del = 0";

            using var connection = _context.CreateConnection();

            var result = connection.QuerySingleOrDefault<T>(query);

            return result;
        }

        public bool SoftDelete(int id)
        {
            var query = $"UPDATE {_tableName} SET is_del = 1 WHERE id = {id}";

            using var connection = _context.CreateConnection();

            var result = connection.Execute(query);

            return result > 0 ? true : false;
        }

        public T Update(T model)
        {
            var query = $"UPDATE {_tableName} SET {string.Join(", ", _columnNamesForUpdate)} WHERE id = @Id AND is_del = 0";

            using var connection = _context.CreateConnection();

            connection.Execute(query, model);

            return model;
        }

        public IEnumerable<object> ExecuteQuery(string query, object parameters)
        {
            using var connection = _context.CreateConnection();

            var result = connection.Query(query, parameters);

            return result;
        }
    }
}