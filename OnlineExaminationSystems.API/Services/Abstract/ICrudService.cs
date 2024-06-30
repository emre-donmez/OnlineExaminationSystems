using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface ICrudService<T> where T : IEntity
    {
        IEnumerable<T> GetAll();

        T? GetById(int id);

        T Create(object updateRequestModel);

        bool SoftDelete(int id);

        T Update(int id, object updateRequestModel);

        bool Delete(int id);

        IEnumerable<T> BulkInsert(IEnumerable<object> updateRequestModels);

        void BulkDelete(IEnumerable<int> ids);
    }
}