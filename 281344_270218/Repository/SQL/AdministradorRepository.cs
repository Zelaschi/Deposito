using Domain;
using Microsoft.Data.SqlClient;
using Repository.SQL.Exceptions;


namespace Repository.SQL
{
    public class AdministradorRepository : IRepository<Administrador>
    {
        private DepositoContext _repositorio;

        public AdministradorRepository(DepositoContext repositorio) 
        {
            try {
                _repositorio = repositorio;
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }
        public Administrador Add(Administrador admin)
        {
            try {
                _repositorio.Personas.Add(admin);
                _repositorio.SaveChanges();
                return _repositorio.Personas.OfType<Administrador>().FirstOrDefault(c => c.Mail == admin.Mail);
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
                Administrador adminABorrar = _repositorio.Personas.OfType<Administrador>().FirstOrDefault(c => c.PersonaId == id);
                _repositorio.Personas.Remove(adminABorrar);
                _repositorio.SaveChanges();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Administrador? Find(Func<Administrador, bool> filter)
        {
            try
            {
                return _repositorio.Personas.OfType<Administrador>().FirstOrDefault(filter);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public IList<Administrador> FindAll()
        {
            try
            {
                return _repositorio.Personas.OfType<Administrador>().ToList();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Administrador? Update(Administrador administradorActualizado)
        {
            try
            {
                Administrador adminEncontrado = _repositorio.Personas.OfType<Administrador>().FirstOrDefault();

                if (adminEncontrado != null)
                {
                    _repositorio.Entry(adminEncontrado).CurrentValues.SetValues(administradorActualizado);
                    _repositorio.SaveChanges();
                }
                return Find(x => x.PersonaId == adminEncontrado.PersonaId);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }
    }
}
