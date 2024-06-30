using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
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

        public bool SoftDelete(int id)
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

        public T Update(int id, object updateRequestModel)
        {
            var model = _mapper.Map<T>(updateRequestModel);
            model.Id = id;
            return _repository.Update(model);
        }

        public T Update(T model)
        {
            return _repository.Update(model);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<T> BulkInsert(IEnumerable<object> updateRequestModels)
        {
            var models = _mapper.Map<IEnumerable<T>>(updateRequestModels);
            return _repository.BulkInsert(models);
        }

        public void BulkDelete(IEnumerable<int> ids)
        {
            _repository.BulkDelete(ids);
        }
    }
}