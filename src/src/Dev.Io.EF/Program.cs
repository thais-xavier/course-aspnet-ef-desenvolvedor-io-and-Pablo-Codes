﻿using Dev.Io.EF.Domain;
using Dev.Io.EF.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Dev.Io.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            //     using var db = new Data.ApplicationContext();

            //     var existe = db.Database.GetPendingMigrations().Any();
            //     if(existe)
            //     {

            //     }
            // InserirDados();
            //InserirDadosEmMassa();
            // ConsultarDados();
            // CasdastrarPedido();
            // ConsultarPedidoCarregamentoAdiantado();
            // AtualizarDados();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 3 };
            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();

        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Cliente Alterado Passo 2";

            // db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos.Include(p=>p.Itens).ThenInclude(p=>p.Produto).ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CasdastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                // IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
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
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes.Where(p => p.Id>0).ToList();
            foreach(var cliente in consultaPorMetodo)
            {
                
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadotiaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Rafael Almeida",
                CEP = "99999000",
                Cidade = "Itabaina",
                Estado = "SE",
                Telefone = "99000001111",
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadotiaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            db.Entry(produto).State = EntityState.Added;
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

    }
}