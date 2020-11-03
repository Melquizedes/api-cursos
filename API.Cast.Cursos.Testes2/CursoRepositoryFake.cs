using API.Cast.Cursos.Models;
using API.Cast.Cursos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace API.Cast.Cursos.Testes
{

    public class CursoRepositoryFake : ICursoRepositoryFake
    {
        public readonly List<Curso> cursos;

        public CursoRepositoryFake()
        {
            cursos = new List<Curso>()
           {
                new Curso() { Id = 1, CategoriaId = 2, DataInicio = DateTime.Now,
                    DataTermino = DateTime.Now, QntAlunos = 40, Descricao = "CURSO DE JAVA SPRING 1" },
               new Curso() {Id = 2, CategoriaId = 1, DataInicio = DateTime.Now,
                   DataTermino = DateTime.Now, QntAlunos = 40, Descricao = "CURSO DE JAVA SPRING 2" },
               new Curso() {Id = 3, CategoriaId = 3, DataInicio = DateTime.Now,
                   DataTermino = DateTime.Now, QntAlunos = 40, Descricao = "CURSO DE JAVA SPRING 3" },
               new Curso() {Id = 4, CategoriaId = 4, DataInicio = DateTime.Now,
                   DataTermino = DateTime.Now, QntAlunos = 40, Descricao = "CURSO DE JAVA SPRING 4" }
           };
        }

        public bool Adicionar(Curso curso)
        {
            curso.Id = GeraId();
            if (ValidarCurso(curso))
            {
                cursos.Add(curso);
                return true;
            }
            return false;
        }

        public void Alterar(Curso cursoAlt, Curso curso)
        {
            var index = cursos.FindIndex(c => c.Id == cursoAlt.Id);
            cursos[index] =  PreencherCurso(cursoAlt, curso);
        }

        public IEnumerable<Curso> ListarCursos()
        {
            return cursos;
        }

        public IEnumerable<Curso> ListarCursosDesc(string desc)
        {
           return cursos.Where(c => c.Descricao.Contains(desc)).ToList();
        }

        public Curso ObterPorId(int id)
        {
            return cursos.Where(c => c.Id == id).FirstOrDefault();
        }

        public Curso PreencherCurso(Curso cursoAlt, Curso novoCurso)
        {
            cursoAlt.Descricao = novoCurso.Descricao;
            cursoAlt.DataInicio = novoCurso.DataInicio;
            cursoAlt.DataTermino = novoCurso.DataTermino;
            cursoAlt.QntAlunos = novoCurso.QntAlunos;
            cursoAlt.CategoriaId = novoCurso.CategoriaId;
            return cursoAlt;
        }

        public void RemoverCurso(Curso curso)
        {
            cursos.Remove(curso);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Curso curso)
        {
            throw new NotImplementedException();
        }

        public bool ValidarCurso(Curso curso)
        {
            var listaCursos = cursos.Where(c => curso.DataInicio > c.DataInicio && curso.DataInicio < c.DataTermino ||
            curso.DataTermino > c.DataInicio && curso.DataTermino < c.DataTermino ||
            curso.DataInicio <= c.DataInicio && curso.DataTermino >= c.DataTermino).ToList();
            if (listaCursos.Any())
                return false;
            return true;
        }

        static int GeraId()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        public bool ExisteCurso(Curso curso)
        {
            if (curso == null)
                return false;
            return true;
        }
    }
}
