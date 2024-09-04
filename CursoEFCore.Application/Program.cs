using System;
using CursoEFCore.Model.Models;
using CursoEFCore.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Faz a Migration
            // StartMigration();

            // Insere o Registro no banco de Dados 
            // InserirDados();
            //InserirDadosEmMassa();
            ConsultarDados();
        }
        private static void ConsultarDados()
        {
            using var db = new Data.Context.ApplicationContext();
            // Modos de Consulta os dois retornam o mesmo valor porem um é por sintaxe e o outro por Método
            // 1º Modo de Consulta por Sintaxe
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            // 2º Modo de Consulta por Método
            var consultaPorMetodo = db.Clientes
            .Where(p => p.Id > 0)
            .OrderBy(p => p.Id > 0)
            .ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando o Cliente: {cliente.Id}");
               // db.Clientes.Find(cliente.Id);
               db.Clientes.FirstOrDefault( p => p.Id == cliente.Id);
            }


        }

        private static void StartMigration()
        {

            using var db = new Data.Context.ApplicationContext();

            var existsPendingMigrations = db.Database.GetPendingMigrations().Any();
            if (existsPendingMigrations)
            {
                Console.WriteLine("Existe Migration Pendente, Não foi possível Aplicar as Novas Migrations");
            }
            else
            {
                db.Database.Migrate();
            }
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345612345651",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.Context.ApplicationContext();

            /*  Formas de rastrear para Inserir o Produto no Banco de Dados 
             *  As 1ª e 2ª Formas São as mais indicadas 
             */
            // 1ª Forma
            db.Produtos.Add(produto);
            // 2ª Forma 
            //db.Set<Produto>().Add(produto);
            // 3ª Forma 
            //db.Entry(produto).State = EntityState.Added;
            // 4ª Forma 
            //db.Add(produto);

            // Pega tudo que esta sendo rastreado pelo Entity framework para enviar para o Banco de Dados
            var registros = db.SaveChanges();
            Console.WriteLine($"Total de registro(s): {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345612345651",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Produto Teste",
                CEP = "13060611",
                Cidade = "Campinas",
                Estado = "SP",
                Telefone = "19992922054"
            };

            using var db = new Data.Context.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total de registro(s): {registros}");
        }

    }
}