using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IQuestionsService : ICrudService<Question>
    {
        IEnumerable<QuestionGetResponseModel> GetAll();

        QuestionGetResponseModel GetById(int id);

        IEnumerable<QuestionGetResponseModel> GetQuestionsByExamId(int examId);
    }
}