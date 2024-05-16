using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IQuestionsService : ICrudService<Question>
    {
        IEnumerable<Question> GetQuestionsByExamId(int examId);
        IEnumerable<QuestionGetResponseModel> GetQuestionsByExamIdForExam(int examId);
    }
}