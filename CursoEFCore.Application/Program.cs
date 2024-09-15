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
            // InserirDadosEmMassa();
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoCarregamentoAdiantado();
            // AtualizarDados();
            RemoverRegistro();
        }
        
        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 3 };
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(1);

            var cliente = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado Passo 3",
                Telefone = "7966669999"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }
        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                  new PedidoItem {
                    ProdutoId = produto.Id,
                    Desconto = 0,
                    Quantidade = 1,
                    Valor = 10,
                  }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
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
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void StartMigration()
        {

            using var db = new Data.ApplicationContext();

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

            using var db = new Data.ApplicationContext();

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

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total de registro(s): {registros}");
        }
    }
}