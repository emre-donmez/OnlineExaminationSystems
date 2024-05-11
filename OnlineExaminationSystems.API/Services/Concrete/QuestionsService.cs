using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class QuestionsService : CrudService<Question>, IQuestionsService
    {
        public QuestionsService(IGenericRepository<Question> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public new IEnumerable<QuestionGetResponseModel> GetAll()
        {
            var questions = _repository.GetAll();
            return _mapper.Map<IEnumerable<QuestionGetResponseModel>>(questions);
        }

        public new QuestionGetResponseModel GetById(int id)
        {
            var question = _repository.GetById(id);
            return _mapper.Map<QuestionGetResponseModel>(question);
        }

        public IEnumerable<QuestionGetResponseModel> GetQuestionsByExamId(int examId)
        {
            var query = "SELECT TOP (SELECT question_count FROM exams WHERE id = @ExamId) id AS Id,question_text AS QuestionText,wrong_answer_1 AS Option1,wrong_answer_2 AS Option2,wrong_answer_3 AS Option3,correct_answer AS CorrectAnswer,exam_id AS ExamId FROM Questions where exam_id =@ExamId ORDER BY NEWID()";

            var parameters = new { ExamId = examId };
            var questions = _repository.ExecuteQuery(query, parameters);

            return _mapper.Map<IEnumerable<QuestionGetResponseModel>>(questions);
        }
    }
}