using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using System;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirDados();
        }

        public static void InserirDados()
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
    }
}
