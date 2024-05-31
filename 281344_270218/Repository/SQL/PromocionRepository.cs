using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SQL
{
    public class PromocionRepository : IRepository<Promocion> 
    {
        public DepositoContext _repositorio;

        public PromocionRepository(DepositoContext repositorio)
        {
            _repositorio = repositorio;
        }

        public Promocion Add(Promocion promocion)
        {
            _repositorio.Add(promocion);
            _repositorio.SaveChanges();
            return _repositorio.Promociones.FirstOrDefault(p => p.PromocionId == promocion.PromocionId);
        }

        public void Delete(int id)
        {
            Promocion promocionABorrar = _repositorio.Promociones.FirstOrDefault(p => p.PromocionId == id);
            _repositorio.Promociones.Remove(promocionABorrar);
            _repositorio.SaveChanges();
        }

        public Promocion? Find(Func<Promocion, bool> filter)
        {
            return _repositorio.Promociones.FirstOrDefault(filter);
        }

        public IList<Promocion> FindAll()
        {
            return _repositorio.Promociones.ToList();
        }

        public Promocion? Update(Promocion promoActualizada)
        {
            Promocion promocionEncontrada = Find(p => p.PromocionId == promoActualizada.PromocionId);

            if (promocionEncontrada != null)
            {
                _repositorio.Entry(promocionEncontrada).CurrentValues.SetValues(promoActualizada);
                _repositorio.SaveChanges();
            }
            return Find(p => p.PromocionId == promoActualizada.PromocionId);
        }
    }

}
