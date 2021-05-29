using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MuralRecados.Models;
using MuralRecados.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuralRecados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuralRecadoController : ControllerBase
    {
        private readonly ILogger<MuralRecadoController> _logger;
        private readonly IMuralRecadosRepository _repository;

        public MuralRecadoController(ILogger<MuralRecadoController> logger, IMuralRecadosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetRecados()
        {
            try
            {
                var result = await _repository.GetRecados();
                return Ok(result.Count() == 0 ? "Nenhum Recado Encontrado" : result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> PutRecado([FromBody] Recado recado)
        {
            try
            {
                var result = await _repository.PutRecado(recado);

                return Ok("Recado inserido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var result = await _repository.PutUsuario(usuario);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteRecado([FromBody] Recado recado)
        {
            try
            {
                var result = await _repository.DeleteRecado(recado.RecadoId);

                return Ok("Recado removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
