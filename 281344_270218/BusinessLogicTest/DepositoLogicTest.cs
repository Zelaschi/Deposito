using Domain;
using Repository;
using BusinessLogic;


namespace BusinessLogicTest
{
    [TestClass]
    public class DepositoLogicTest
    {
        private DepositoLogic? _depositoLogic;
        private IRepository<Deposito>? _depositoRepository;
        private Deposito? deposito1;
        private int idDeposito1 = 1;
        private Deposito? deposito2;
        private int idDeposito2 = 2;

        [TestInitialize]

        public void setup ()
        {
            _depositoRepository = new DepositoMemoryRepository();
            _depositoLogic = new DepositoLogic(_depositoRepository);

            var area1 = "A";
            var tamanio1 = "Pequenio";
            bool climatizacion1 = false;

            var area2 = "C";
            var tamanio2 = "Mediano";
            bool climatizacion2 = true;

            deposito1 = new Deposito(idDeposito1, area1, tamanio1, climatizacion1);
            deposito2 = new Deposito(idDeposito2, area2, tamanio2, climatizacion2);
        }
    }
}
