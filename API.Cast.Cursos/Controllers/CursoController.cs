using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using API.Cast.Cursos.Models;
using API.Cast.Cursos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.FileProviders;

namespace API.Cast.Cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _repository;

        public CursoController(ICursoRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Retorna a lista com todos os cursos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Curso>> ListarCursos()
        {
            IEnumerable<Curso> cursos = await _repository.ListarCursos();
            if (cursos == null)
                return NoContent();

            return Ok(cursos);
        }

        /// <summary>
        /// Busca cursos com base na descricão e retorna uma lista de cursos.
        /// </summary>
        /// <param name="desc">Descricao do curso</param>
        /// <returns></returns>
        [HttpGet("buscar")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Curso>> ObterPorDescricao([FromQuery] string desc)
        {
            IEnumerable<Curso> cursos = await _repository.ListarCursosDesc(desc);
            if (cursos.Count() == 0)
                return NoContent();

            return Ok(cursos);
        }

        /// <summary>
        /// Busca cursos com base na categoria que ele pertence.
        /// </summary>
        /// <param name="idCategoria">Id da categoria</param>
        /// <returns></returns>
        [HttpGet("categoria")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Curso>> ObterPorCategoria([FromQuery] int idCategoria)
        {
            IEnumerable<Curso> cursos = await _repository.ListarCursosPorCateg(idCategoria);
            if (cursos.Count() == 0)
                return NotFound("Não existem cursos cadastrados na categoria selecionada.");

            return Ok(cursos);
        }

        /// <summary>
        /// Retorna um curso com base no id do curso.
        /// </summary>
        /// <param name="id">Curso Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Curso>> ObterPorId(int id)
        {
            Curso curso = new Curso();
            if (id <= 0)
            {
                return BadRequest("O codigo do curso precisa ser maior que 0.");
            }
            else 
                curso = await _repository.ObterPorId(id);
            if (!ExisteCurso(curso))
                return NotFound("Não existemm cursos cadastrados com esse codigo.");
            else
                return Ok(curso);
        }

        

        /// <summary>
        /// Adiciona um novo curso.
        /// </summary>
        /// <param name="curso">Curso</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarCurso([FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest("Todos os campos com * são obrigatórios.");
            else
            {
                if (curso.DataInicio > DateTime.Now && curso.DataInicio < curso.DataTermino)
                {
                    if (await _repository.Adicionar(curso))
                    {
                        _repository.Save();
                        return Ok("Curso adicionado com sucesso!");
                    }
                    else
                        return BadRequest("Existe(m) curso(s) planejados(s) dentro do período informado.");
                }
                else
                    return BadRequest("A data inicio não pode ser menor que a data atual, nem maior que a data termino.");
            }
        }

        /// <summary>
        /// Update an existing clients
        /// </summary>
        /// <param name="id">Curso id</param>
        /// <param name="novoCurso">Curso</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlterarCurso(int id, [FromBody] Curso novoCurso)
        {
            Curso cursoAlt = await _repository.ObterPorId(id);
            if (!ExisteCurso(cursoAlt))
                return NotFound("Não existemm cursos cadastrados com esse codigo.");
            else if(!ModelState.IsValid)
                return BadRequest("Todos os campos com * são obrigatórios.");
            else
            {
                _repository.Alterar(cursoAlt, novoCurso);
                _repository.Save();

                return Ok("Curso alterado com sucesso!");
            }           
        }

        /// <summary>
        /// Remove um curso pelo id do curso.
        /// </summary>
        /// <param name="id">Curso id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExcluirCurso(int id)
        {
            Curso curso = await _repository.ObterPorId(id);
            if (!ExisteCurso(curso))
                return NotFound("Não existemm cursos cadastrados com esse codigo.");   
            
            _repository.RemoverCurso(curso);
            _repository.Save();
            return Ok("Curso Removido Com Sucesso!");     
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool ExisteCurso(Curso curso)
        {
            if (curso == null)
                return false;
            return true;
        }
    }
}
