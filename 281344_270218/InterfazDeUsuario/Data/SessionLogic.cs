using Domain;
using Repository; 

namespace InterfazDeUsuario.Data
{
    public class SessionLogic
    {
        public SessionLogic(IRepository<Cliente> clienteRepository) 
        {
            _clienteRepository = clienteRepository;
        }

        private readonly IRepository<Cliente> _clienteRepository;

        public Cliente clienteActual { get; set; }  

        public void Login(Cliente cliente)
        {
            clienteActual = cliente;
        }

        public bool ValidarCredenciales(Cliente cliente)
        {
            var clienteExistente = _clienteRepository.Find(x => x.Mail.Equals(cliente.Mail));
            return clienteExistente != null;
        }

        public void LogOut()
        {
            clienteActual = null;
        }
    }
}
