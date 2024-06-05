
using Domain;
using Repository.SQL;

namespace BusinessLogic
{
    public interface IExportador 
    {
        void Exportar(List<Reserva> reservas);
    }
}
