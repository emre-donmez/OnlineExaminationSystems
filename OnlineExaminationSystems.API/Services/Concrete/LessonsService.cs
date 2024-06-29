using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class LessonsService : CrudService<Lesson>, ILessonsService
    {
        public LessonsService(IGenericRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public IEnumerable<LessonWithUserResponseModel> GetAllWithUser()
        {
            var parentTable = "Lessons";
            var childTable = "Users";
            var foreignKey = "responsible_user_id";
            var parentColumns = "Lessons.id AS Id,Lessons.Name,Lessons.responsible_user_id AS UserId";
            var childColumns = "Users.id AS Id,Users.Name,Users.Surname,Users.Email,Users.Password,Users.role_id AS RoleId";

            return _repository.GetAllWithRelated<LessonWithUserResponseModel, User>(
                parentTable,
                childTable,
                foreignKey,
                parentColumns,
                childColumns,
                (lesson, user) =>
                {
                    lesson.User = user;
                    return lesson;
                }
            );
        }

        public IEnumerable<Lesson> GetLessonsByUserId(int userId)
        {
            var query = $"SELECT id AS Id,Name,responsible_user_id AS UserId FROM Lessons WHERE responsible_user_id =@UserId";
            var parameters = new { UserId = userId };

            return _repository.ExecuteQuery(query, parameters);
        }

        public async Task<bool> IsAnyUser(int id)
        {
            var query = $"SELECT 1 FROM Users WHERE id =@Id";
            var parameters = new { Id = id };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() != 0;
        }
    }
}