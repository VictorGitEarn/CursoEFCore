using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            //db.Clientes.Remove(cliente);
            db.Remove(cliente);

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Atualizado em testes";

            //db.Clientes.Update(cliente); se utilizar esse método a entidade inteira vai ser atualizada
            db.SaveChanges(); //se utilizar esse método, somente os dados que foram modificados serão atualizados.
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                .ToList();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id); //Pode trazer o item que está na memória após ter uma lista daquela entidade // caso quiser pode colocar o AsNoTracking na classe de pesquisa
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id); //Sempre executa uma consulta no BD
            }
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123456789456",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();

            db.Add(produto);
            var registro = db.SaveChanges();
            Console.WriteLine($"Total de registro(s): {registro}");
        }

        private static void InserirDadosEmMassa()
        {

            var produto = new Produto
            {
                Descricao = "Produto Teste 2",
                CodigoBarras = "1234567456",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Victor Alves",
                CEP = "04444600",
                Cidade = "IlhaBela",
                Estado = "SP",
                Telefone = "11569984254"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();

            Console.WriteLine($"Total de registro(s): {registros}");
        }
    }
}
