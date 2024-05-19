using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface ILessonsService : ICrudService<Lesson>
    {
        IEnumerable<Lesson> GetLessonsByUserId(int userId);
        Task<bool> IsAnyUser(int id);
    }
}