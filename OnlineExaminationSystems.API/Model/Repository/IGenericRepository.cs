using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Model.Repository
{
    public interface IGenericRepository<T> where T : IEntity
    {
        T? GetById(int id);
        IEnumerable<T> GetAll();
        T Insert(T model);
        T Update(T model);
        bool SoftDelete(int id);

    }
}
