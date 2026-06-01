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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            if (turista == null)
                return NotFound();

            return Ok(turista);
        }
    }
}
