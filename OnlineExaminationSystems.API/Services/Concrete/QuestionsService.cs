﻿using AutoMapper;
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

        public IEnumerable<Question> GetQuestionsByExamId(int examId)
        {
            var query = "SELECT id AS Id,question_text AS QuestionText,wrong_answer_1 AS Option1,wrong_answer_2 AS Option2,wrong_answer_3 AS Option3,correct_answer AS CorrectAnswer,exam_id AS ExamId FROM Questions where exam_id =@ExamId";

            var parameters = new { ExamId = examId };
            var questions = _repository.ExecuteQuery(query, parameters);

            return questions;
        }

        public IEnumerable<QuestionGetResponseModel> GetQuestionsByExamIdForExam(int examId)
        {
            var query = "SELECT TOP (SELECT CASE WHEN EXISTS (SELECT 1 FROM exams WHERE id = @ExamId) THEN (SELECT question_count FROM exams WHERE id = @ExamId) ELSE 0  END)  id AS Id,question_text AS QuestionText,wrong_answer_1 AS Option1,wrong_answer_2 AS Option2,wrong_answer_3 AS Option3,correct_answer AS CorrectAnswer,exam_id AS ExamId FROM Questions where exam_id =@ExamId ORDER BY NEWID()";

            var parameters = new { ExamId = examId };
            var questions = _repository.ExecuteQuery(query, parameters);

            return _mapper.Map<IEnumerable<QuestionGetResponseModel>>(questions);
        }
    }
}