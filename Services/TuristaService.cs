using SpaceCare.Domain.Turistas;
using SpaceCare.Domain.Turistas.Dtos;
using SpaceCare.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace SpaceCare.Services
{
    public class TuristaService
    {
        private readonly AppDbContext _context;

        public TuristaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Turista> CadastrarTurista(CadastroTuristaDto dados)
        {
            var turista = new Turista
            {
                Nome = dados.Nome,
                PassaporteEspacial = dados.PassaporteEspacial,
                DataNascimento = dados.DataNascimento,
                Email = dados.Email,
                HistoricoMedico = dados.HistoricoMedico,
                DataCadastro = DateTime.UtcNow,
                Ativo = "1"
            };

            _context.Turistas.Add(turista);
            await _context.SaveChangesAsync();
            return turista;
        }

        public async Task<List<ListagemTurista>> ListarTuristas()
        {
            return await _context.Turistas
                .Where(t => t.Ativo == "1")
                .Select(t => new ListagemTurista(t.Id, t.Nome, t.Email))
                .ToListAsync();
        }

        public async Task<DetalharTurista?> DetalharTurista(int id)
        {
            var turista = await _context.Turistas.FirstOrDefaultAsync(t => t.Id == id && t.Ativo == "1");
            if (turista == null) return null;

            return new DetalharTurista(
                Id: turista.Id,
                Nome: turista.Nome,
                PassaporteEspacial: turista.PassaporteEspacial,
                DataNascimento: turista.DataNascimento,
                Email: turista.Email,
                HistoricoMedico: turista.HistoricoMedico,
                DataCadastro: turista.DataCadastro
            );
        }

        public async Task<DetalharTurista?> AtualizarTurista(AtualizarTurista dados)
        {
            var turista = await _context.Turistas.FirstOrDefaultAsync(t => t.Id == dados.Id && t.Ativo == "1");
            if (turista == null) return null;

            if (!string.IsNullOrWhiteSpace(dados.Nome))
                turista.Nome = dados.Nome;

            if (!string.IsNullOrWhiteSpace(dados.PassaporteEspacial))
                turista.PassaporteEspacial = dados.PassaporteEspacial;

            if (!string.IsNullOrWhiteSpace(dados.Email))
                turista.Email = dados.Email;

            if (!string.IsNullOrWhiteSpace(dados.HistoricoMedico))
                turista.HistoricoMedico = dados.HistoricoMedico;

            await _context.SaveChangesAsync();

            return new DetalharTurista(
                turista.Id,
                turista.Nome,
                turista.PassaporteEspacial,
                turista.DataNascimento,
                turista.Email,
                turista.HistoricoMedico,
                turista.DataCadastro
            );
        }

        public async Task<bool> ExcluirTurista(int id)
        {
            var turista = await _context.Turistas.FirstOrDefaultAsync(t => t.Id == id && t.Ativo == "1");
            if (turista == null) return false;

            turista.Ativo = "0";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
