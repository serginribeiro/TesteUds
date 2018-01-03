using AplicativoWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace AplicativoWeb.Controllers
{
    public class VendasController : Controller
    {
        private ContextoBanco db = new ContextoBanco();

        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(x => x.Pessoa);

            return View(pedidos);
        }

        public ActionResult Inserir()
        {
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome");

            return View();
        }


        public ActionResult InserirItem(Guid pedidoId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = String.Empty, Value = "" });

            foreach (var produto in db.Produtos)
            {
                items.Add(new SelectListItem { Text = produto.Nome, Value = produto.Id.ToString() });
            }
            ViewBag.PedidoId = pedidoId;
            ViewBag.ProdutoId = new SelectList(items, "Value", "Text");

            return View("InserirItensPedido");
        }

        public ActionResult InserirItemPedido(ItemPedido itemPedido)
        {
            db.ItensPedidos.Add(itemPedido);
            db.SaveChanges();

            AdicionarValorPedido(itemPedido.PedidoId ,itemPedido.Total);

            return RedirectToAction("Visualizar", new { id = itemPedido.PedidoId });
        }

        private void AdicionarValorPedido(Guid pedidoId, double total)
        {
            var pedido = db.Pedidos.Where(x => x.Id == pedidoId).FirstOrDefault();
            pedido.Total += total;
            db.SaveChanges();
        }

        public ActionResult Deletar(Guid id)
        {
            var pedido = (from pedidos in db.Pedidos
                           where pedidos.Id == id
                           select pedidos).FirstOrDefault();
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExcluirItem(Guid Id)
        {
            var itemProduto = db.ItensPedidos.Where(x => x.Id == Id).FirstOrDefault();

            var idPedido = itemProduto.PedidoId;

            var valor = itemProduto.Total;

            db.ItensPedidos.Remove(itemProduto);
            db.SaveChanges();

            RetirarValorPedido(idPedido, valor);

            return RedirectToAction("Visualizar", new { id = idPedido });

        }

        private void RetirarValorPedido(Guid pedidoId, double total)
        {
            var pedido = db.Pedidos.Where(x => x.Id == pedidoId).FirstOrDefault();
            pedido.Total -= total;
            db.SaveChanges();
        }


        public ActionResult InserirPedido(Pedido pedido)
        {
            db.Pedidos.Add(pedido);
            db.SaveChanges();

            return RedirectToAction("Visualizar", new { id = pedido.Id });
        }

        public ActionResult Visualizar(Guid id)
        {
            var pedido = (from pedidos in db.Pedidos.Include(i => i.ItensPedidos)
                          where pedidos.Id == id
                          select pedidos).FirstOrDefault();

            return View(pedido);
        }

        public ActionResult BuscarPedidoPorNome(String nome)
        {
            var pedido = (from pedidos in db.Pedidos
                          where pedidos.Pessoa.Nome.ToUpper().Contains(nome.ToUpper())
                          select pedidos);
            return View("Index", pedido);
        }

        public ActionResult BuscarPedidoPorNumero(int numero)
        {
            var pedido = (from pedidos in db.Pedidos
                          where pedidos.Numero == numero
                          select pedidos);
            return View("Index", pedido);
        }

        public ActionResult BuscarPedidoPorTotal(int total)
        {
            var pedido = (from pedidos in db.Pedidos
                          where pedidos.Total == total
                          select pedidos);
            return View("Index", pedido);
        }

    }
}