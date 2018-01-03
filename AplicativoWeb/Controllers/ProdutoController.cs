using AplicativoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicativoWeb.Controllers
{
    public class ProdutoController : Controller
    {
        private ContextoBanco db = new ContextoBanco();
       
        public ActionResult Index()
        {
            var produto = (from produtos in db.Produtos
                           select produtos);
            return View(produto);
        }

        public ActionResult Inserir()
        {

            return View();
        }

        public ActionResult InserirProduto(Produto produto)
        {
            db.Produtos.Add(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Editar(Guid id)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Id == id
                           select produtos).FirstOrDefault();
            return View(produto);
        }

        public ActionResult Salvar(Produto produto)
        {
            var produtoBusca = (from produtos in db.Produtos
                                where produtos.Id == produto.Id
                                select produtos).FirstOrDefault();
            produtoBusca.Nome = produto.Nome;
            produtoBusca.Codigo = produto.Codigo;
            produtoBusca.PrecoUnitario = produto.PrecoUnitario;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Deletar(Guid id)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Id == id
                           select produtos).FirstOrDefault();
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BuscarProdutoPorCodigo(String codigo)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Codigo.ToUpper().Contains(codigo.ToUpper())
                           select produtos);
            return View("Index", produto);
           
        }

        public Boolean BuscarProdutoDuplicadoPorCodigo(String codigo)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Codigo.ToUpper() == codigo.ToUpper()
                           select produtos);
            if (produto.Count() > 0)
                return true;

            return false;
        }

        public Boolean BuscarProdutoDuplicadoPorCodigoId(String codigo, Guid identificador)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Codigo.ToUpper() == codigo.ToUpper()
                           where produtos.Id != identificador
                           select produtos);
            if (produto.Count() > 0)
                return true;

            return false;
        }

        public ActionResult BuscarProdutoPorNome(String nome)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Nome.ToUpper().Contains(nome.ToUpper())
                           select produtos);
            return View("Index", produto);
        }

        public Boolean BuscarProdutoDuplicadoPorNome(String nome)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Nome.ToUpper() == nome.ToUpper()
                           select produtos);
            if (produto.Count() > 0)
                return true;

            return false;
        }

        public Boolean BuscarProdutoDuplicadoPorNomeId(String nome, Guid identificador)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Nome.ToUpper() == nome.ToUpper()
                           where produtos.Id != identificador
                           select produtos);
            if (produto.Count() > 0)
                return true;

            return false;
        }

        public ActionResult BuscarProdutoPorPreco(Double precoUnitario)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.PrecoUnitario == precoUnitario
                           select produtos);
            return View("Index", produto);
        }

        public double BuscarPrecoProduto(Guid identificador)
        {
            var produto = (from produtos in db.Produtos
                           where produtos.Id == identificador
                           select produtos).FirstOrDefault();

            return produto.PrecoUnitario;
        }
        
    }
}