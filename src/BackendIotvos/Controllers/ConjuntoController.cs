using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Enumerations;
using BackendIotvos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendIotvos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConjuntoController : ControllerBase
    {
        private readonly IConjuntoService _conjuntoService;

        public ConjuntoController(IConjuntoService conjuntoService)
        {
            _conjuntoService = conjuntoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConjuntoDto>>> GetConjuntos()
        {
            IEnumerable<ConjuntoDto> conjuntosResponse = await _conjuntoService.GetAllConjuntosAsync();
            return Ok(conjuntosResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConjuntoDto>> GetConjuntoById(Guid id)
        {
            ConjuntoDto? conjuntoResponse = await _conjuntoService.GetConjuntoByIdAsync(id);

            if (conjuntoResponse == null)
            {
                return NotFound();
            }

            return Ok(conjuntoResponse);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ConjuntoDto>>> GetConjuntoByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("O parâmetro 'name' é inválido ou está em branco.");
            }

            IEnumerable<ConjuntoDto> conjuntosResponse = await _conjuntoService.GetConjuntoByNameAsync(name);
            return Ok(conjuntosResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostConjunto(ConjuntoAddViewModel conjunto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            Guid newConjuntoId = await _conjuntoService.AddConjuntoAsync(conjunto);

            string resourceUrl = $"/conjunto/{newConjuntoId}";

            return Created(resourceUrl, null);
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusConjunto(Guid id, StatusConjunto status)
        {
            try
            {
                await _conjuntoService.AlterarStatusConjuntoAsync(id, status);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Conjunto não encontrado");
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateConjunto(Guid id, ConjuntoUpdateViewModel conjunto)
        {
            if (id != conjunto.Id)
            {
                return BadRequest("ID do Conjunto inválido ou incompatível.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            try
            {
                await _conjuntoService.UpdateConjuntoAsync(conjunto);
                return Ok("Conjunto atualizado com sucesso");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConjunto(Guid id)
        {
            try
            {
                await _conjuntoService.DeleteConjuntoAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir o Conjunto.");
            }
        }
    }
}
