using Repository;
using Domain;
using Repository.SQL;

namespace BusinessLogic
{
    public class PromocionLogic
    {
        private readonly PromocionRepository _repository;

        public PromocionLogic(PromocionRepository promocionRepository)
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
            return _repository.Find(x => x.PromocionId == IdParametro);
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
