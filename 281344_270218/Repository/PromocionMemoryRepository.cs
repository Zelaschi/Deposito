using Domain;

namespace Repository
{
    public class PromocionMemoryRepository : IRepository<Promocion>
    {
        private List<Promocion> _promociones= new List<Promocion>();
        public Promocion Add(Promocion promocion)
        {
            _promociones.Add(promocion);
            return promocion;
        }

        public void Delete(int id)
        {
            _promociones.RemoveAll(x => x.IdPromocion == id);
        }

        public Promocion? Find(Func<Promocion, bool> filter)
        {
            return _promociones.Where(filter).FirstOrDefault();
        }

        public IList<Promocion> FindAll()
        {
            return _promociones;
        }

        public Promocion? Update(Promocion promoActualizada)
        {
            Promocion promocionEncontrada = Find(x => x.IdPromocion == promoActualizada.IdPromocion);

            if (promocionEncontrada != null)
            {
                promocionEncontrada.Etiqueta = promoActualizada.Etiqueta;
                promocionEncontrada.PorcentajeDescuento = promoActualizada.PorcentajeDescuento;
                promocionEncontrada.FechaInicio = promoActualizada.FechaInicio;
                promocionEncontrada.FechaFin = promoActualizada.FechaFin;

            }
            return promocionEncontrada;
        }
    }
}
