using Asp.Versioning;
using Cliente.Application.DTOs;
using Cliente.Application.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.Api.Conttrollers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clientes")]
    public class ClientController(IClienteUseCase clienteUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await clienteUseCase.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var cliente = await clienteUseCase.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] string name)
        {
            var cliente = await clienteUseCase.SearchAsync(name);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddClienteDto clienteDto)
        {
            await clienteUseCase.AddAsync(clienteDto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, UpdateClienteDto clienteDto)
        {
            var wasDeleted = await clienteUseCase.UpdateAsync(id, clienteDto);
            if (!wasDeleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var wasDeleted = await clienteUseCase.DeleteAsync(id);
            if (!wasDeleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
