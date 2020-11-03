using API.Cast.Cursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cast.Cursos.Testes
{
    public interface ICursoRepositoryFake
    {
        bool Adicionar(Curso curso);

        void Alterar(Curso cursoAlt, Curso curso);

        IEnumerable<Curso> ListarCursos();

        IEnumerable<Curso> ListarCursosDesc(string desc);

        Curso ObterPorId(int id);

        void RemoverCurso(Curso curso);

        void Save();

        void Update(Curso curso);

        public bool ValidarCurso(Curso curso);

        public Curso PreencherCurso(Curso cursoAlt, Curso novoCurso);
    }
}
