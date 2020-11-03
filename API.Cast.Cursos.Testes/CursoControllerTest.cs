using API.Cast.Cursos.Controllers;
using API.Cast.Cursos.Models;
using API.Cast.Cursos.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace API.Cast.Cursos.Testes
{
     public class CursoControllerTest
    {
        CursoController _controller;
        ICursoRepositoryFake _repository = new CursoRepositoryFake();

        public CursoControllerTest()
        {  
            _controller = new CursoController();
        }

        [Fact]
        public void Lista_QuandoChamado_RetornaOk()
        {
            // Act
            var okResult = _controller.ListarCursos();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Lista_QuandoChamado_Items()
        {
            // Act
            var okResult = _controller.ListarCursos().Result;   

            // Assert
            var items = Assert.IsType<List<Curso>>(okResult.Value);
            Assert.Equal(6, items.Count);
        }

        
    }
}
