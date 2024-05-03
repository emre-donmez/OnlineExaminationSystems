using AutoMapper;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Repository;

namespace OnlineExaminationSystems.API.Services
{
    public class CrudService<T> : ICrudService<T> where T : IEntity
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;
        public CrudService(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public T Create(object updateRequestModel)
        {
            var model = _mapper.Map<T>(updateRequestModel);
            return _repository.Insert(model);
        }

        public T Create(T model)
        {
            return _repository.Insert(model);
        }

        public bool Delete(int id)
        {
            return _repository.SoftDelete(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T Update(object updateRequestModel)
        {
            var model = _mapper.Map<T>(updateRequestModel);
            return _repository.Update(model);
        }

        public T Update(T model) 
        {
            return _repository.Update(model);
        }
    }
}
