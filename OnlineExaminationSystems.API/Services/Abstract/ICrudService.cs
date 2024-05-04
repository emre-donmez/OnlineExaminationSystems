using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface ICrudService<T> where T : IEntity
    {
        IEnumerable<T> GetAll();

        T? GetById(int id);

        T Create(object updateRequestModel);

        bool Delete(int id);

        T Update(int id, object updateRequestModel);
    }
}