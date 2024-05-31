using Domain;

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
            throw new NotImplementedException();

            _repositorio.Personas.Add(admin);
            _repositorio.SaveChanges();
            //return _repositorio.Clientes.FirstOrDefault(c => c.Mail == cliente.Mail);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Administrador? Find(Func<Administrador, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Administrador> FindAll()
        {
            throw new NotImplementedException();
        }

        public Administrador? Update(Administrador administradorActualizado)
        {
            throw new NotImplementedException();
        }
    }
}
