using API.Cast.Cursos.Data;
using API.Cast.Cursos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cast.Cursos.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoContext _storage = new CursoContext();

        public async Task<bool> Adicionar(Curso curso)
        {      
            if (ValidarCurso(curso))
            {
                await _storage.Set<Curso>().AddAsync(curso);
                return true;
            }
            return false;
        }

        public void Alterar(Curso cursoAlt, Curso novoCurso)
        {
            _storage.Update(PreencherCurso(cursoAlt, novoCurso));
        }

        public async Task<IEnumerable<Curso>> ListarCursos()
        {
            return await _storage.Cursos.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Curso>> ListarCursosDesc(string desc)
        {
            return await _storage.Cursos.Where(c => c.Descricao.Contains(desc)).Include(c => c.Categoria).ToListAsync();
        }

        public async Task<IEnumerable<Curso>> ListarCursosPorCateg(int id)
        {
            return await _storage.Cursos.Where(c => c.CategoriaId == id).Include(c => c.Categoria).ToListAsync();
        }

        public async Task<Curso> ObterPorId(int id)
        {
            Curso curso = await _storage.Cursos.FindAsync(id);
            return curso;
        }

        public void RemoverCurso(Curso curso)
        {
            _storage.Remove(curso);
        }

        public void Save()
        {
            _storage.SaveChangesAsync();
        }

        public void Update(Curso curso)
        {
            _storage.Set<Curso>().Attach(curso);
        }

        public  bool ValidarCurso(Curso curso)
        {
            var listaCursos =  _storage.Cursos.Where(c => curso.DataInicio > c.DataInicio && curso.DataInicio < c.DataTermino ||
            curso.DataTermino > c.DataInicio && curso.DataTermino < c.DataTermino ||
            curso.DataInicio <= c.DataInicio && curso.DataTermino >= c.DataTermino).ToList();
            if (listaCursos.Any())
                return false;
            return true;
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

    }
}
