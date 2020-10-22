using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cast.Cursos.Models
{
    public class Curso
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "É necessario colocar uma descrição de curso.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "É necessario colocar uma data de inicio.")]
        [DataType(DataType.Date)]
        public DateTime? DataInicio { get; set; }
        [Required(ErrorMessage = "É necessario colocar uma data de término.")]
        [DataType(DataType.Date)]
        public DateTime? DataTermino { get; set; }

        public int QntAlunos { get; set; }


        [Required(ErrorMessage = "É necessario selecionar uma categoria.")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
