﻿using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            // Faz a chamada para o método .Listar();
            return _funcionarioRepository.Listar();
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            // Faz a chamada para o método .Cadastrar();
            _funcionarioRepository.Cadastrar(novoFuncionario);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto funcionarioBuscado que irá receber o gênero buscado no banco de dados
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (funcionarioBuscado == null)
            {
                // Caso não seja encontrado, retorna um status code 404 com a mensagem personalizada
                return NotFound("Nenhum funcionario encontrado");
            }

            // Caso seja encontrado, retorna o gênero buscado
            return Ok(funcionarioBuscado);
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FuncionarioDomain funcionarioAtualizado)
        {
            // Cria um objeto funcionarioBuscado que irá receber o gênero buscado no banco de dados
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (funcionarioBuscado == null)
            {
                // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
                // e um bool para representar que houve erro
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionario não encontrado",
                            erro = true
                        }
                    );
            }

            // Tenta atualizar o registro
            try
            {
                // Faz a chamada para o método .AtualizarIdUrl();
                _funcionarioRepository.AtualizarIdUrl(id, funcionarioAtualizado);

                // Retorna um status code 204 - No Content
                return NoContent();
            }
            // Caso ocorra algum erro
            catch (Exception erro)
            {
                // Retorna BadRequest e o erro
                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar();
            _funcionarioRepository.Deletar(id);

            // Retorna um status code com uma mensagem personalizada
            return Ok("Funcionario deletado");
        }
    }
}
