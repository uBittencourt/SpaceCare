using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceCare.Domain.Turistas.Dtos;
using SpaceCare.Services;

namespace SpaceCare.Controllers
{
    [ApiController]
    [Route("turistas")]
    public class TuristaController : ControllerBase
    {
        private readonly TuristaService _service;

        public TuristaController(TuristaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarTurista([FromBody] CadastroTuristaDto dados)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var turistaCadastrado = await _service.CadastrarTurista(dados);
                return CreatedAtAction(nameof(DetalharTurista), new { id = turistaCadastrado.Id }, turistaCadastrado);
            }
            catch (DbUpdateException)
            {
                return Conflict(new { mensagem = "Já existe um turista cadastrado com este passaporte espacial." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarTuristas()
        {
            var lista = await _service.ListarTuristas();
            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> DetalharTurista(int id)
        {
            var turista = await _service.DetalharTurista(id);
            if (turista == null) return NotFound();
            return Ok(turista);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarTurista([FromBody] AtualizarTurista dados)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var turistaAtualizado = await _service.AtualizarTurista(dados);
                if (turistaAtualizado == null) return NotFound(new { mensagem = "Turista não encontrado." });
                return Ok(turistaAtualizado);
            }
            catch (DbUpdateException)
            {
                return Conflict(new { mensagem = "O e-mail ou passaporte informado já está em uso por outro passageiro." });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ExcluirTurista(int id)
        {
            var excluido = await _service.ExcluirTurista(id);
            if (!excluido) return NotFound(new { mensagem = "Turista não encontrado." });
            return NoContent();
        }
    }
}
