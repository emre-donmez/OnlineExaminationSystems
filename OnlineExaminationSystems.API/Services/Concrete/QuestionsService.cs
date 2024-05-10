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
    }
}
