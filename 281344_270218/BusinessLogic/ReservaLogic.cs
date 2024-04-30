using Repository;
using Domain;

namespace BusinessLogic
{
    public class ReservaLogic
    {
        private readonly IRepository<Reserva> _repository;

        public ReservaLogic(IRepository<Reserva> reservaRepository)
        {
            _repository = reservaRepository;
        }


    }
}
