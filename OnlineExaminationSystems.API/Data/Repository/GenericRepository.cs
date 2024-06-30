using Dapper;
using OnlineExaminationSystems.API.Data.Context;
using OnlineExaminationSystems.API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace OnlineExaminationSystems.API.Data.Repository;

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
        (_columnNamesWithAttribute, _columnNames, _columnNamesForSelection, _columnNamesForUpdate) = GetColumnNamesAll();
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
        var query = $"SELECT {string.Join(',', _columnNamesForSelection)} FROM {_tableName}";

        using var connection = _context.CreateConnection();
        var result = connection.Query<T>(query);

        return result;
    }

    public T? GetById(int id)
    {
        var query = $"SELECT {string.Join(',', _columnNamesForSelection)} FROM {_tableName} WHERE id = {id}";

        using var connection = _context.CreateConnection();

        var result = connection.QueryFirstOrDefault<T>(query);

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
        var query = $"UPDATE {_tableName} SET {string.Join(", ", _columnNamesForUpdate)} WHERE id = @Id";

        using var connection = _context.CreateConnection();

        connection.Execute(query, model);

        return model;
    }

    public IEnumerable<T> ExecuteQuery(string query, object parameters)
    {
        using var connection = _context.CreateConnection();

        var result = connection.Query<T>(query, parameters);

        return result;
    }

    public T ExecuteQueryFirstOrDefault(string query, object parameters)
    {
        using var connection = _context.CreateConnection();

        var result = connection.QueryFirstOrDefault<T>(query, parameters);

        return result;
    }

    public bool Delete(int id)
    {
        var query = $"DELETE FROM {_tableName} WHERE id = {id}";

        using var connection = _context.CreateConnection();

        var result = connection.Execute(query);

        return result > 0 ? true : false;
    }

    public void ExecuteStoredProcedure(string storedProcedure, object parameters)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<TParent> GetAllWithRelated<TParent, TChild>(
        string parentTable,
        string childTable,
        string foreignKey,
        string parentColumns,
        string childColumns,
        Func<TParent, TChild, TParent> map,
        string where = "")
        where TParent : class, IEntity
    {
        var query = $@"
            SELECT {parentColumns}, {childColumns}
            FROM {parentTable}
            INNER JOIN {childTable} ON {parentTable}.{foreignKey} = {childTable}.id";

        if (!string.IsNullOrEmpty(where))
            query += $" WHERE {where}";

        using var connection = _context.CreateConnection();

        var parentDictionary = new Dictionary<int, TParent>();

        var result = connection.Query<TParent, TChild, TParent>(
            query,
            (parent, child) =>
            {
                if (!parentDictionary.TryGetValue(parent.Id, out var parentEntry))
                {
                    parentEntry = parent;
                    parentDictionary.Add(parentEntry.Id, parentEntry);
                }

                map(parentEntry, child);

                return parentEntry;
            },
            splitOn: "id");

        return result.Distinct().ToList();
    }

    public IEnumerable<TParent> GetAllWithRelated<TParent, TChild1, TChild2>(
        string parentTable,
        string childTable1,
        string childTable2,
        string foreignKey1,
        string foreignKey2,
        string parentColumns,
        string childColumns1,
        string childColumns2,
        Func<TParent, TChild1, TChild2, TParent> map,
        string where = "")
        where TParent : class, IEntity
    {
        var query = $@"
        SELECT {parentColumns}, {childColumns1}, {childColumns2}
        FROM {parentTable}
        INNER JOIN {childTable1} ON {parentTable}.{foreignKey1} = {childTable1}.id
        INNER JOIN {childTable2} ON {parentTable}.{foreignKey2} = {childTable2}.id";

        if (!string.IsNullOrEmpty(where))
            query += $" WHERE {where}";

        using var connection = _context.CreateConnection();

        var parentDictionary = new Dictionary<int, TParent>();

        var result = connection.Query<TParent, TChild1, TChild2, TParent>(
            query,
            (parent, child1, child2) =>
            {
                if (!parentDictionary.TryGetValue(parent.Id, out var parentEntry))
                {
                    parentEntry = parent;
                    parentDictionary.Add(parentEntry.Id, parentEntry);
                }

                map(parentEntry, child1, child2);

                return parentEntry;
            },
            splitOn: "id");

        return result.Distinct().ToList();
    }

    public IEnumerable<T> BulkInsert(IEnumerable<T> items)
    {
        var query = $"INSERT INTO {_tableName} ({string.Join(',', _columnNamesWithAttribute)}) VALUES (@{string.Join(", @", _columnNames)})";

        using var connection = _context.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            connection.Execute(query, items, transaction: transaction);
            transaction.Commit();
            return items;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void BulkDelete(IEnumerable<int> ids)
    {
        var query = $"DELETE FROM {_tableName} WHERE id IN @Ids";

        using var connection = _context.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            connection.Execute(query, new { Ids = ids }, transaction: transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}