﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroDeHoras.Api;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250307183137_ClassePausa")]
    partial class ClassePausa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RegistroDeHoras.Model.Pausa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataDeCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeModificacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TarefaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Termino")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("Pausa");
                });

            modelBuilder.Entity("RegistroDeHoras.Model.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataDeCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("HorasDePausaRaw")
                        .HasColumnType("bigint");

                    b.Property<long>("HorasUtilizadasRaw")
                        .HasColumnType("bigint")
                        .HasColumnName("HorasDePausa");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroAtividade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Solucao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusDaTarefa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Termino")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("RegistroDeHoras.Model.Pausa", b =>
                {
                    b.HasOne("RegistroDeHoras.Model.Tarefa", "Tarefa")
                        .WithMany("Pausas")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("RegistroDeHoras.Model.Tarefa", b =>
                {
                    b.Navigation("Pausas");
                });
#pragma warning restore 612, 618
        }
    }
}
