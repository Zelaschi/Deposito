using Domain;
using Microsoft.EntityFrameworkCore;


public class DepositoContext : DbContext
{
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Deposito> Depositos { get; set; }
    public DbSet<Promocion> Promociones { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<DepositoPromocion> DepositoPromocions {get;set;}
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<FechasNoDisponible> FechasNoDisponibles { get; set; }

    public DepositoContext() 
    {
    }
    public DepositoContext(DbContextOptions<DepositoContext> options) : base(options) {
        if (!Database.IsInMemory()) 
        {
            Database.Migrate();
        }

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {       
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DepositoDB;User Id=sa;Password=Passw1rd;");
       
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //tph
        modelBuilder.Entity<Persona>()
            .ToTable("Persona")
            .HasDiscriminator<string>("tipoUsuario")
            .HasValue<Cliente>("Cliente")
            .HasValue<Administrador>("Administrador");

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(p => p.PersonaId);
            entity.HasAlternateKey(p => p.Mail);
            entity.Property(p => p.NombreYApellido).IsRequired();
            entity.Property(p => p.Password).IsRequired();


        });
        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(p => p.PromocionId);
            entity.Property(p => p.Etiqueta).IsRequired();
            entity.Property(p => p.PorcentajeDescuento).IsRequired();
            entity.Property(p => p.FechaInicio).IsRequired();
            entity.Property(p => p.FechaFin).IsRequired();
        });
        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(r => r.ReservaId);
            entity.Property(r => r.FechaDesde).IsRequired();
            entity.Property(r => r.FechaHasta).IsRequired();
            entity.Property(r => r.Precio).IsRequired();
            entity.Property(r => r.Estado).IsRequired();
            entity.Property(r => r.JustificacionRechazo);

            entity.HasOne(r => r.Cliente)
                  .WithMany(c => c.Reservas)
                  .HasForeignKey(r => r.ClienteId);

            entity.HasOne(r => r.Deposito)
                  .WithMany(d => d.Reservas)
                  .HasForeignKey(r => r.DepositoId);

            entity.HasOne(r => r.PromocionAplicada)
                  .WithMany(p => p.Reservas)
                  .HasForeignKey(r => r.PromocionId)
                  .IsRequired(false);

            entity.HasOne(r => r.Pago)
                  .WithOne(p => p.Reserva)
                  .HasForeignKey<Pago>(p => p.ReservaId)
                  .IsRequired(false)
                  .OnDelete(DeleteBehavior.Cascade);

        });
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(p => p.PagoId);
        });

        modelBuilder.Entity<Deposito>(entity =>
        {
            entity.HasKey(d => d.DepositoId);
            entity.Property(d => d.Area).IsRequired();
            entity.Property(d => d.Tamanio).IsRequired();
            entity.Property(d => d.Climatizacion).IsRequired();
            entity.Property(d => d.Nombre).IsRequired();
            entity.HasMany(d => d.fechasNoDisponibles) // Nombre de la propiedad de navegación en la clase Deposito
                .WithOne(f => f.Deposito)            // Propiedad de navegación en la clase FechasNoDisponible
                .HasForeignKey(f => f.DepositoId);
        });
        modelBuilder.Entity<DepositoPromocion>(entity =>
        {
            entity.HasKey(dp => new { dp.PromocionId, dp.DepositoId });

            entity.HasOne(dp => dp.Deposito)
                .WithMany(d => d.DepositoPromocions)
                .HasForeignKey(dp => dp.DepositoId);

            entity.HasOne(dp => dp.Promocion)
                .WithMany(p => p.DepositoPromocions)
                .HasForeignKey(dp => dp.PromocionId);
        });

        modelBuilder.Entity<FechasNoDisponible>()
            .HasOne<Deposito>(d => d.Deposito)
            .WithMany(f => f.fechasNoDisponibles)
            .HasForeignKey(d => d.DepositoId);
    }



}