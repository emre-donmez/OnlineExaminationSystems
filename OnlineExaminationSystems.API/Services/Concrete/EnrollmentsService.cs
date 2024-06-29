using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete;

public class EnrollmentsService : CrudService<Enrollment>, IEnrollmentsService
{
    public EnrollmentsService(IGenericRepository<Enrollment> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public IEnumerable<EnrollmentWithLessonResponseModel> GetEnrollmentsWithLessonByUserId(int id)
    {
        var parentTable = "Enrollments";
        var childTable = "Lessons";
        var foreignKey = "user_id";
        var parentColumns = "Enrollments.id AS Id,Enrollments.user_id AS UserId,Enrollments.lesson_id AS LessonId";
        var childColumns = "Lessons.id AS Id,Lessons.Name,Lessons.responsible_user_id AS UserId";
        var where = $"user_id = {id}";

        return _repository.GetAllWithRelated<EnrollmentWithLessonResponseModel, Lesson>(
            parentTable,
            childTable,
            foreignKey,
            parentColumns,
            childColumns,
            (enrollment, lesson) =>
            {
                enrollment.Lesson = lesson;
                return enrollment;
            },
            where
        );
    }

    public IEnumerable<EnrollmentWithUserResponseModel> GetStudentsWithUserByLessonId(int id)
    {
        var parentTable = "Enrollments";
        var childTable = "Users";
        var foreignKey = "user_id";
        var parentColumns = "Enrollments.id AS Id,Enrollments.user_id AS UserId,Enrollments.lesson_id AS LessonId";
        var childColumns = "Users.id AS Id,Users.Name,Users.Surname,Users.Email,Users.Password,Users.role_id AS RoleId";
        var where = $"lesson_id = {id}";

        return _repository.GetAllWithRelated<EnrollmentWithUserResponseModel, User>(
            parentTable,
            childTable,
            foreignKey,
            parentColumns,
            childColumns,
            (enrollment, user) =>
            {
                enrollment.User = user;
                return enrollment;
            },
            where
        );
    }
}