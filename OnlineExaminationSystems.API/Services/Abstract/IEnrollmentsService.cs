using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IEnrollmentsService : ICrudService<Enrollment>
    {
        IEnumerable<Enrollment> GetEnrollmentsByUserId(int id);
        IEnumerable<Enrollment> GetStudentsByLessonId(int id);
    }
}
