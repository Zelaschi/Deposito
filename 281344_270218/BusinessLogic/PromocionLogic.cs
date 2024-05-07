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

        public Promocion? buscarPromocionPorId(int IdParametro)
        {
            return _repository.Find(x => x.IdPromocion == IdParametro);
        }

        public void EliminarPromocion(int id)
        {
            _repository.Delete(id);
        }

        public Promocion ActualizarInfoPromocion(Promocion promocionActualizada)
        {
            return _repository.Update(promocionActualizada);
        }
    }
}
