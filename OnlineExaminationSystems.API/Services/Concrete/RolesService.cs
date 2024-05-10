using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class RolesService : CrudService<Role>, IRolesService
    {
        public RolesService(IGenericRepository<Role> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
