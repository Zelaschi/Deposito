using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteLogicTest
    {
        public ClienteLogic _clienteLogic;
        private IRepository<Cliente> _clienteRepository;

        [TestInitialize]
        public void setUp() {
            _clienteRepository = new ClienteMemoryRepository();
            _clienteLogic = new ClienteLogic(_clienteRepository);
        }

    }
}
