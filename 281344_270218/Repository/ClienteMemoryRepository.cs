using Domain;
using System.ComponentModel;

namespace Repository
{
    public class ClienteMemoryRepository : IRepository<Cliente>
    {
        private List<Cliente> _clientes = new List<Cliente>();
        public Cliente Add(Cliente cliente)
        {
            _clientes.Add(cliente);
            return cliente;
        }

        public void Delete(int id)
        {
            _clientes.RemoveAll(x => x.IdPersona == id);
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            return _clientes.Where(filter).FirstOrDefault();
        }

        public IList<Cliente> FindAll()
        {
            return _clientes;
        }

        public Cliente? Update(Cliente clienteActualizado)
        {
            Cliente clienteEncontrado = Find(x => x.IdPersona == clienteActualizado.IdPersona);

            if (clienteEncontrado != null)
            {
                clienteEncontrado.NombreYApellido = clienteActualizado.NombreYApellido;
                clienteEncontrado.Mail = clienteActualizado.Mail;
                clienteEncontrado.Password = clienteActualizado.Password;

            }
            return clienteEncontrado;
        }
    }
}
