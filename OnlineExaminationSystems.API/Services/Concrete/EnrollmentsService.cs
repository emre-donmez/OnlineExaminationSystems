using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class EnrollmentsService : CrudService<Enrollment>, IEnrollmentsService
    {
        public EnrollmentsService(IGenericRepository<Enrollment> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
