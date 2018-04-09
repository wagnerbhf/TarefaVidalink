using CadastroTarefa.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CadastroTarefa.Repository
{
    public class TarefaRepository
    {
        #region Membros privados

        private VidalinkEntities db;

        #endregion Membros privados

        #region Construtor

        public TarefaRepository()
        {
            db = new VidalinkEntities();
        }

        #endregion Construtor

        #region Métodos públicos

        public void Inserir(Tarefa tarefa)
        {
            db.Tarefa.Add(tarefa);
            db.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa)
        {
            db.Entry(tarefa).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Excluir(Tarefa tarefa)
        {
            db.Tarefa.Remove(tarefa);
            db.SaveChanges();
        }

        public Tarefa Pesquisar(int? id)
        {
            if (id == null)
                return null;

            Tarefa tarefa = db.Tarefa.Find(id);
            return tarefa;
        }
        
        public List<Tarefa> Filtrar(string filtro)
        {
            return db.Tarefa
                .Where(t => t.Descricao.Contains(filtro) || t.Titulo.Contains(filtro))
                .ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion Métodos públicos
    }
}