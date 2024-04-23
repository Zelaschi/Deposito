
using Repository;
using Domain;

namespace BusinessLogic
{
    public class ClienteLogic
    {
        private readonly IRepository<Cliente> _repository;

        public ClienteLogic(IRepository<Cliente> clienteRepository)
        {
            _repository = clienteRepository;
        }
        public Cliente AddMovie(Cliente cliente)
        {
            return _repository.Add(cliente);
        }
    }
}
