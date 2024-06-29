using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IEnrollmentsService : ICrudService<Enrollment>
    {
        IEnumerable<EnrollmentWithLessonResponseModel> GetEnrollmentsWithLessonByUserId(int id);

        IEnumerable<EnrollmentWithUserResponseModel> GetStudentsWithUserByLessonId(int id);
    }
}