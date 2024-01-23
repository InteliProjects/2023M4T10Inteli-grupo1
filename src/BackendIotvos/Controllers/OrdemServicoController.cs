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
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoService _ordemServicoService;

        public OrdemServicoController(IOrdemServicoService ordemServicoService)
        {
            _ordemServicoService = ordemServicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServicoDto>>> GetOrdemServicos()
        {
            IEnumerable<OrdemServicoDto> ordemServicosResponse = await _ordemServicoService.GetAllOrdemServicosAsync();
            return Ok(ordemServicosResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServicoDto>> GetOrdemServicoById(Guid id)
        {
            OrdemServicoDto? ordemServicoResponse = await _ordemServicoService.GetOrdemServicoByIdAsync(id);

            if (ordemServicoResponse == null)
            {
                return NotFound();
            }

            return Ok(ordemServicoResponse);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<OrdemServicoDto>>> GetOrdemServicoByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("O parâmetro 'name' é inválido ou está em branco.");
            }

            IEnumerable<OrdemServicoDto> ordemServicosResponse = await _ordemServicoService.GetOrdemServicoByNameAsync(name);
            return Ok(ordemServicosResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrdemServico(OrdemServicoAddViewModel ordemServico)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            Guid newOrdemServicoId = await _ordemServicoService.AddOrdemServicoAsync(ordemServico);

            string resourceUrl = $"/ordemservico/{newOrdemServicoId}";

            return Created(resourceUrl, null);
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusOrdemServico(Guid id, StatusOrdemServico status)
        {
            try
            {
                await _ordemServicoService.AlterarStatusOrdemServicoAsync(id, status);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("OrdemServico não encontrada");
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateOrdemServico(Guid id, OrdemServicoUpdateViewModel ordemServico)
        {
            if (id != ordemServico.Id)
            {
                return BadRequest("ID da OrdemServico inválido ou incompatível.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            try
            {
                await _ordemServicoService.UpdateOrdemServicoAsync(ordemServico);
                return Ok("OrdemServico atualizada com sucesso");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdemServico(Guid id)
        {
            try
            {
                await _ordemServicoService.DeleteOrdemServicoAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir a OrdemServico.");
            }
        }
    }
}
