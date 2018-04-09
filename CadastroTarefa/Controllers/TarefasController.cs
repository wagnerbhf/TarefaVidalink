using CadastroTarefa.Models;
using CadastroTarefa.Repository;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace CadastroTarefa.Controllers
{
    public class TarefasController : Controller
    {
        #region Membros privados

        private TarefaRepository _tarefaRepository = new TarefaRepository();

        #endregion Membros privados

        #region Métodos públicos
        public ActionResult Index(string pesquisarString)
        {
            pesquisarString = pesquisarString ?? string.Empty;
            ViewBag.CurrentFilter = pesquisarString;            
            List<Tarefa> tarefas = _tarefaRepository.Filtrar(pesquisarString);
            
            return View(tarefas);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tarefa tarefa = _tarefaRepository.Pesquisar(id);

            if (tarefa == null)
                return HttpNotFound();

            return View(tarefa);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "Titulo,Descricao,DataExecucao")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _tarefaRepository.Inserir(tarefa);
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tarefa tarefa = _tarefaRepository.Pesquisar(id);

            if (tarefa == null)
                return HttpNotFound();

            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Titulo,Descricao,DataExecucao")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _tarefaRepository.Atualizar(tarefa);
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tarefa tarefa = _tarefaRepository.Pesquisar(id);

            if (tarefa == null)
                return HttpNotFound();

            return View(tarefa);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirm(int id)
        {
            Tarefa tarefa = _tarefaRepository.Pesquisar(id);
            _tarefaRepository.Excluir(tarefa);

            return RedirectToAction("Index");
        }

        #endregion Métodos públicos

        #region Métodos protegidos

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _tarefaRepository.Dispose();

            base.Dispose(disposing);
        }

        #endregion Métodos protegidos
    }
}