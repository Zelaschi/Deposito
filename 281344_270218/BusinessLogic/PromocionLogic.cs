using Repository;
using Domain;

namespace BusinessLogic
{
    public class PromocionLogic
    {
        private readonly IRepository<Promocion> _repository;

        public PromocionLogic(IRepository<Promocion> promocionRepository)
        {
            _repository = promocionRepository;
        }
    }
}
