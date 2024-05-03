using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Repository;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface ICrudService<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        T Create(object updateRequestModel);
        bool Delete(int id);
        T Update(int id,object updateRequestModel);
    }
}
