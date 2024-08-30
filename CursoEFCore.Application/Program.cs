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
            InserirDados();
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
    }
}