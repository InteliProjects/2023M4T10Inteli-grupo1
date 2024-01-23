using BackendIotvos.Domain.DTOs.Responses;
using BackendIotvos.Domain.DTOs.ViewModels;
using BackendIotvos.Domain.Enumerations;
using BackendIotvos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendIotvos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            IEnumerable<ItemDto> itemsResponse = await _itemService.GetAllItemsAsync();

            return Ok(itemsResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(Guid id)
        {
            ItemDto? itemResponse = await _itemService.GetItemByIdAsync(id);

            if (itemResponse == null)
            {
                return NotFound();
            }

            return Ok(itemResponse);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("O parametro 'name' e invalido ou em branco.");
            }

            IEnumerable<ItemDto> itemsResponse = await _itemService.GetItemByNameAsync(name);

            return Ok(itemsResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(ItemAddViewModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }

                Guid newItemId = await _itemService.AddItemAsync(item);

                string resourceUrl = $"/item/{newItemId}";

                return Created(resourceUrl, null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("alterarstatus/{id}")]
        public async Task<IActionResult> AlterarStatusItem(Guid id, StatusItem status)
        {
            try
            {
                await _itemService.AlterarStatusItemAsync(id, status);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Item não encontrado");
            }
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> UpdateMaterial(Guid id, ItemUpdateViewModel item)
        {
            if (id != item.Id)
            {
                return BadRequest("ID do item invalido ou incompativel.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            try
            {
                await _itemService.UpdateItemAsync(item);
                return Ok("Item atualizado com sucesso");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                await _itemService.DeleteItemAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao excluir o item.");
            }
        }
    }
}
