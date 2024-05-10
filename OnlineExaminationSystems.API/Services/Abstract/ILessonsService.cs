using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface ILessonsService : ICrudService<Lesson>
    {
        Task<bool> IsAnyUser(int id);
    }
}
