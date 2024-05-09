using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class LessonsService : CrudService<Lesson>, ILessonsService
    {
        public LessonsService(IGenericRepository<Lesson> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<bool> IsAnyUser(int id)
        {
            var query = $"SELECT 1 FROM Users WHERE id == @Id";
            var parameters = new { Id = id };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() == 0;
        }
    }
}
