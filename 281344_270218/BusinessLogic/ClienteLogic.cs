
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

        public void validarClienteMailNoRepetido(Cliente clienteParametro) {
            if (_repository.Find(clienteBuscado => clienteBuscado.Mail == clienteParametro.Mail ) != null)
            {
                throw new InvalidOperationException("Mail Repetido!");
            }
        }
        public Cliente AddCliente(Cliente cliente)
        {
            validarClienteMailNoRepetido(cliente);
            return _repository.Add(cliente);
        }

        public IList<Cliente> GetAll()
        {
            return _repository.FindAll();
        }

        public Cliente? buscarClientePorId(int IdParametro) {
            return _repository.Find(x => x.IdPersona == IdParametro);
        }
        public Cliente? buscarClientePorMail(string MailParametro)
        {
            return _repository.Find(x => x.Mail.Equals(MailParametro));
        }
        
    }
}
