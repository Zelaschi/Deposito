using Domain;
using System.Linq;

namespace Repository.SQL
{
    public class AdministradorRepository : IRepository<Administrador>
    {
        private DepositoContext _repositorio;

        public AdministradorRepository(DepositoContext repositorio) 
        {
            _repositorio = repositorio;
        }
        public Administrador Add(Administrador admin)
        {
            _repositorio.Personas.Add(admin);
            _repositorio.SaveChanges();
            return _repositorio.Personas.OfType<Administrador>().FirstOrDefault(c => c.Mail == admin.Mail);
        }

        public void Delete(int id)
        {
            Administrador adminABorrar = _repositorio.Personas.OfType<Administrador>().FirstOrDefault(c => c.PersonaId == id);
            _repositorio.Personas.Remove(adminABorrar);
            _repositorio.SaveChanges();
        }

        public Administrador? Find(Func<Administrador, bool> filter)
        {
            return _repositorio.Personas.OfType<Administrador>().FirstOrDefault(filter);
        }

        public IList<Administrador> FindAll()
        {
            return _repositorio.Personas.OfType<Administrador>().ToList();
        }

        public Administrador? Update(Administrador administradorActualizado)
        {
            Administrador adminEncontrado = _repositorio.Personas.OfType<Administrador>().FirstOrDefault();

            if (adminEncontrado != null)
            {
                _repositorio.Entry(adminEncontrado).CurrentValues.SetValues(administradorActualizado);
                _repositorio.SaveChanges();
            }
            return Find(x => x.PersonaId == adminEncontrado.PersonaId);
        }
    }
}
