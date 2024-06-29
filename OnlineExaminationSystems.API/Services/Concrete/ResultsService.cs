using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete;

public class ResultsService : CrudService<Result>, IResultsService
{
    public ResultsService(IGenericRepository<Result> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public IEnumerable<ResultWithUserAndExamResponseModel> GetResultsWithUserAndExamByExamId(int examId)
    {
        var parentTable = "Results";
        var childTable1 = "Users";
        var childTable2 = "Exams";
        var foreignKey1 = "user_id";
        var foreignKey2 = "exam_id";
        var parentColumns = "Results.id AS Id,Results.user_id AS UserId,Results.exam_id AS ExamId,Results.Score";
        var childColumns1 = "Users.id AS Id,Users.Name,Users.Surname,Users.Email,Users.Password,Users.role_id AS RoleId";
        var childColumns2 = "Exams.id AS Id,Exams.Name,Exams.lesson_id AS LessonId,Exams.question_count AS QuestionCount,Exams.Duration,Exams.started_date AS StartedDate";
        var where = $"exam_id = {examId}";

        return _repository.GetAllWithRelated<ResultWithUserAndExamResponseModel, User, Exam>(
            parentTable,
            childTable1,
            childTable2,
            foreignKey1,
            foreignKey2,
            parentColumns,
            childColumns1,
            childColumns2,
            (result, user, exam) =>
            {
                result.User = user;
                result.Exam = exam;
                return result;
            },
            where
        );
    }

    public void CalculateResults(int examId)
    {
        var parameters = new { exam_id = examId };
        _repository.ExecuteStoredProcedure("CalculateExamResult", parameters);
    }
}