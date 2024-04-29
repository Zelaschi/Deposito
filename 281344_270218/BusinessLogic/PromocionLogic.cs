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
        public Promocion AgregarPromocion(Promocion promo)
        {
            return _repository.Add(promo);
        }

        public IList<Promocion> listarTodasLasPromociones()
        {
            return _repository.FindAll();
        }
    }
}
