﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroDeHoras.Api;

#nullable disable

namespace RegistroDeHoras.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250116193853_AjusteNoDateTimeDoTarefaModelv2")]
    partial class AjusteNoDateTimeDoTarefaModelv2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("RegistroDeHoras.Model.Tarefa", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cliente")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataDeCriacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataDeModificacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HorasDePausa")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("HorasUtilizadas")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroAtividade")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Pausa")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Reinicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("StatusDaTarefa")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Termino")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
