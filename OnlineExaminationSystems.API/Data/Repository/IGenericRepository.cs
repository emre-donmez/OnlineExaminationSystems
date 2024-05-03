using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Model.Repository
{
    /// <summary>
    /// Represents a generic repository for managing entities of type T using Dapper.
    /// </summary>
    /// <typeparam name="T">The type of entities managed by the repository.</typeparam>
    public interface IGenericRepository<T> where T : IEntity
    {
        /// <summary>
        /// Retrieves a record from the database table by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to retrieve.</param>
        /// <returns>The record with the specified ID, or null if not found.</returns>
        T? GetById(int id);

        /// <summary>
        /// Retrieves all records from the database table.
        /// </summary>
        /// <returns>An IEnumerable collection of all records.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Inserts the given model object into the database.
        /// </summary>
        /// <param name="model">The model object to be inserted.</param>
        /// <returns>The updated model object after insertion.</returns>
        T Insert(T model);

        /// <summary>
        /// Updates the specified record in the database table.
        /// </summary>
        /// <param name="model">The model object containing updated data.</param>
        /// <returns>The updated model object after the update operation.</returns>
        T Update(T model);

        /// <summary>
        /// Soft deletes a record from the database table by setting its is_del flag to 1.
        /// </summary>
        /// <param name="id">The ID of the record to delete.</param>
        /// <returns>True if the record is successfully soft deleted, otherwise false.</returns>
        bool SoftDelete(int id);

        /// <summary>
        /// Executes the specified SQL query with the given parameters and returns the result as an IEnumerable<object>.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The parameters to be used in the query.</param>
        /// <returns>An IEnumerable<object> containing the result of the query.</returns>
        IEnumerable<object> ExecuteQuery(string query, object parameters);
    }
}