﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(DepositoContext))]
    partial class DepositoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Deposito", b =>
                {
                    b.Property<int>("DepositoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepositoId"), 1L, 1);

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Climatizacion")
                        .HasColumnType("bit");

                    b.Property<string>("Tamanio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepositoId");

                    b.ToTable("Depositos");
                });

            modelBuilder.Entity("Domain.DepositoPromocion", b =>
                {
                    b.Property<int>("PromocionId")
                        .HasColumnType("int");

                    b.Property<int>("DepositoId")
                        .HasColumnType("int");

                    b.HasKey("PromocionId", "DepositoId");

                    b.HasIndex("DepositoId");

                    b.ToTable("DepositoPromocions");
                });

            modelBuilder.Entity("Domain.Persona", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonaId"), 1L, 1);

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NombreYApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipoUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonaId");

                    b.HasAlternateKey("Mail");

                    b.ToTable("Persona", (string)null);

                    b.HasDiscriminator<string>("tipoUsuario").HasValue("Persona");
                });

            modelBuilder.Entity("Domain.Promocion", b =>
                {
                    b.Property<int>("PromocionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromocionId"), 1L, 1);

                    b.Property<string>("Etiqueta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("PorcentajeDescuento")
                        .HasColumnType("int");

                    b.HasKey("PromocionId");

                    b.ToTable("Promociones");
                });

            modelBuilder.Entity("Domain.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaId"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("DepositoId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaHasta")
                        .HasColumnType("datetime2");

                    b.Property<string>("JustificacionRechazo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<int?>("PromocionId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("DepositoId");

                    b.HasIndex("PromocionId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("Domain.Administrador", b =>
                {
                    b.HasBaseType("Domain.Persona");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("Domain.Cliente", b =>
                {
                    b.HasBaseType("Domain.Persona");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("Domain.DepositoPromocion", b =>
                {
                    b.HasOne("Domain.Deposito", "Deposito")
                        .WithMany("DepositoPromocions")
                        .HasForeignKey("DepositoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Promocion", "Promocion")
                        .WithMany("DepositoPromocions")
                        .HasForeignKey("PromocionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deposito");

                    b.Navigation("Promocion");
                });

            modelBuilder.Entity("Domain.Reserva", b =>
                {
                    b.HasOne("Domain.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Deposito", "Deposito")
                        .WithMany("Reservas")
                        .HasForeignKey("DepositoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Promocion", "PromocionAplicada")
                        .WithMany("Reservas")
                        .HasForeignKey("PromocionId");

                    b.Navigation("Cliente");

                    b.Navigation("Deposito");

                    b.Navigation("PromocionAplicada");
                });

            modelBuilder.Entity("Domain.Deposito", b =>
                {
                    b.Navigation("DepositoPromocions");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("Domain.Promocion", b =>
                {
                    b.Navigation("DepositoPromocions");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("Domain.Cliente", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
