using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class EnrollmentsService : CrudService<Enrollment>, IEnrollmentsService
    {
        public EnrollmentsService(IGenericRepository<Enrollment> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public IEnumerable<Enrollment> GetEnrollmentsByUserId(int id)
        {
            var query = $"SELECT id AS Id,user_id AS UserId,lesson_id AS LessonId FROM Enrollments WHERE user_id = @Id";
            var parameters = new { Id = id };

            return _repository.ExecuteQuery(query, parameters);
        }

        public IEnumerable<Enrollment> GetStudentsByLessonId(int id)
        {
            var query = $"SELECT id AS Id,user_id AS UserId,lesson_id AS LessonId FROM Enrollments WHERE lesson_id = @Id";
            var parameters = new { Id = id };

            return _repository.ExecuteQuery(query, parameters);
        }
    }
}
