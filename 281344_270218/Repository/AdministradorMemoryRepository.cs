using Domain;

namespace Repository
{
    public class AdministradorMemoryRepository : IRepository<Administrador>
    {
        private List<Administrador> _administrador = new List<Administrador>();
        public Administrador Add(Administrador admin)
        {
            _administrador.Add(admin);
            return admin;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Administrador? Find(Func<Administrador, bool> filter)
        {
            return _administrador.Where(filter).FirstOrDefault();
        }

        public IList<Administrador> FindAll()
        {
            return _administrador;
        }

        public Administrador? Update(Administrador administradorActualizado)
        {
            Administrador administradorEncontrado = Find(x => x.PersonaId == administradorActualizado.PersonaId);

            if (administradorEncontrado != null)
            {
                administradorEncontrado.NombreYApellido = administradorActualizado.NombreYApellido;
                administradorEncontrado.Mail = administradorActualizado.Mail;
                administradorEncontrado.Password = administradorActualizado.Password;

            }
            return administradorEncontrado;
        }
    }
}
