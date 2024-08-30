﻿// <auto-generated />
using System;
using CursoEFCore.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CursoEFCore.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240829161442_PrimeiraMigracao")]
    partial class PrimeiraMigracao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CursoEFCore.Model.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("CHAR(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("CHAR(2)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("CHAR(11)")
                        .HasColumnName("Phone");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FinalizadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("IniciadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(512)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TipoFrete")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Desconto")
                        .HasColumnType("numeric");

                    b.Property<int>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PedidoItens", (string)null);
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("CodigoBarras")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("CHAR(60)");

                    b.Property<string>("TipoProduto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.Pedido", b =>
                {
                    b.HasOne("CursoEFCore.Model.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.PedidoItem", b =>
                {
                    b.HasOne("CursoEFCore.Model.Models.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoEFCore.Model.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("CursoEFCore.Model.Models.Pedido", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
