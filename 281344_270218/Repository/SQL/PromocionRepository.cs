using Domain;
using Microsoft.Data.SqlClient;
using Repository.SQL.Exceptions;

namespace Repository.SQL
{
    public class PromocionRepository : IRepository<Promocion> 
    {
        public DepositoContext _repositorio;

        public PromocionRepository(DepositoContext repositorio)
        {
            try
            {
                _repositorio = repositorio;
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Promocion Add(Promocion promocion)
        {
            try
            {
                _repositorio.Add(promocion);
                _repositorio.SaveChanges();
                return _repositorio.Promociones.FirstOrDefault(p => p.PromocionId == promocion.PromocionId);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Promocion promocionABorrar = _repositorio.Promociones.FirstOrDefault(p => p.PromocionId == id);
                _repositorio.Promociones.Remove(promocionABorrar);
                _repositorio.SaveChanges();
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Promocion? Find(Func<Promocion, bool> filter)
        {
            try
            {
                return _repositorio.Promociones.FirstOrDefault(filter);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public IList<Promocion> FindAll()
        {
            try
            {
                return _repositorio.Promociones.ToList();
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Promocion? Update(Promocion promoActualizada)
        {
            try
            {
                Promocion promocionEncontrada = Find(p => p.PromocionId == promoActualizada.PromocionId);

                if (promocionEncontrada != null)
                {
                    _repositorio.Entry(promocionEncontrada).CurrentValues.SetValues(promoActualizada);
                    _repositorio.SaveChanges();
                }
                return Find(p => p.PromocionId == promoActualizada.PromocionId);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }
    }

}
