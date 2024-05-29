using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.Context
{
    public interface DepositoContextFactory
    {
        DepositoContext CrearContext();
    }
    public class DepositoContextEnMemoria : DepositoContextFactory
    {
        public DepositoContext CrearContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DepositoContext>();
            optionsBuilder.UseInMemoryDatabase("TestDB");
            return new DepositoContext(optionsBuilder.Options);

        }
    }
}

