using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Cast.Cursos.Models
{
    public class Categoria
    {
        public int Codigo { get; set; }

        public string Descricao { get; set; }


        [JsonIgnore]
        public List<Curso> Cursos { get; set; }
    }
}
