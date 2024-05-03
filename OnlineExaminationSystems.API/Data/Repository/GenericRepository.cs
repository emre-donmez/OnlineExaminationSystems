using Dapper;
using OnlineExaminationSystems.API.Data.Context;
using OnlineExaminationSystems.API.Model.Entities;
using System.Globalization;
using System.Text;

namespace OnlineExaminationSystems.API.Model.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IEntity
    {
        private readonly DapperContext _context;
        private static readonly string _tableName = GetTableName();
        private static readonly List<string> _columnNames = GetColumnNames();
        private static readonly List<string> _columnNamesForValue = GetColumnNamesForValue();

        public GenericRepository(DapperContext context)
        {
            _context = context;
        }

        private static string GetTableName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeof(T).Name);
            sb.Append('s');

            return sb.ToString();
        }

        private static List<string> GetColumnNamesForValue()
        {
            return typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
        }

        private static List<string> GetColumnNames()
        {
            return typeof(T)
                .GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => ToHyphenCase(p.Name))
                .ToList();
        }

        public static string ToHyphenCase(string input)
        {
            string formatted = new string(input.Select((c, i) => i > 0 && char.IsUpper(c) ? '_' + c.ToString() : c.ToString())
                                                .SelectMany(c => c)
                                                .ToArray());

            CultureInfo cultureInfo = new CultureInfo("tr-TR", false);

            string lowerCase = formatted.ToLower(cultureInfo).Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ö", "o").Replace("ç", "c");

            return lowerCase;
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
            var query = $"SELECT * FROM {_tableName} WHERE is_del = 0";

            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<T>(query);

                return result;
            }
        }

        public T? GetById(int id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE id = {id} AND is_del = 0";

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
            var setValues = _columnNames.Select(prop => $"{prop} = @{prop}");

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