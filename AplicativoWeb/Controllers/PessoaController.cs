using AplicativoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicativoWeb.Controllers
{
    public class PessoaController : Controller
    {
        private ContextoBanco db = new ContextoBanco();
        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoa = (from pessoas in db.Pessoa
                          select pessoas);
            return View(pessoa);
        }

        public ActionResult Inserir()
        {

            return View();
        }

        public ActionResult InserirPessoa(Pessoa pessoa)
        {
            db.Pessoa.Add(pessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Editar(Guid id)
        {
            var pessoa = (from pessoas in db.Pessoa
                          where pessoas.Id == id
                          select pessoas).FirstOrDefault();
            return View(pessoa);
        }

        public ActionResult Salvar(Pessoa pessoa)
        {
            var pessoaBusca = (from pessoas in db.Pessoa
                          where pessoas.Id == pessoa.Id
                          select pessoas).FirstOrDefault();
            pessoaBusca.Nome = pessoa.Nome;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Deletar(Guid id)
        {
            var pessoa = (from pessoas in db.Pessoa
                          where pessoas.Id == id 
                          select pessoas).FirstOrDefault();
            db.Pessoa.Remove(pessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BuscarPessoaPorNome(String nome)
        {
            var pessoa = (from pessoas in db.Pessoa
                           where pessoas.Nome.ToUpper().Contains(nome.ToUpper())
                           select pessoas);
            return View("Index", pessoa);
        }

        public Boolean BuscarPessoaDuplicadaPorNome(String nome)
        {
            var pessoa = (from pessoas in db.Pessoa
                           where pessoas.Nome.ToUpper() == nome.ToUpper()
                           select pessoas);
            if (pessoa.Count() > 0)
                return true;

            return false;
        }

        public Boolean BuscarPessoaDuplicadaPorNomeId(String nome, Guid identificador)
        {
            var pessoa = (from pessoas in db.Pessoa
                           where pessoas.Nome.ToUpper() == nome.ToUpper()
                           where pessoas.Id != identificador
                           select pessoas);
            if (pessoa.Count() > 0)
                return true;

            return false;
        }
    }
}