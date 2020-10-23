using API.Cast.Cursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cast.Cursos.Repositories
{
    public interface ICursoRepository
    {
        Task<bool> Adicionar(Curso curso);

        void Alterar(Curso cursoAlt, Curso curso);

        Task<IEnumerable<Curso>> ListarCursos();

        Task<IEnumerable<Curso>> ListarCursosDesc(string desc);

        Task<Curso> ObterPorId(int id);

        void RemoverCurso(Curso curso);

        void Save();

        void Update(Curso curso);

        public bool ValidarCurso(Curso curso);

        public Curso PreencherCurso(Curso cursoAlt, Curso novoCurso);

        Task<IEnumerable<Curso>> ListarCursosPorCateg(int id);
    }
}
