using Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class DepositoContext : DbContext
{ 
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Deposito> Depositos { get; set; }
    public DbSet<Promocion> Promociones { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Fijarse que la base de datos este creada en DBeaver
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DepositoDB;User Id=sa;Password=Passw1rd;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //tdh
        modelBuilder.Entity<Persona>()
            .ToTable("Persona")
            .HasDiscriminator<string>("tipoUsuario")
            .HasValue<Cliente>("Cliente")
            .HasValue<Administrador>("Administrador");
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(d => d.IdPersona);
        });
        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(d => d.IdPromocion);
        });
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(d => d.IdReserva);
        });

        modelBuilder.Entity<Deposito>(entity =>
        {
            entity.HasKey(d => d.IdDeposito);
        });
    }



}