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
                HistoricoMedico = dados.HistoricoMedico,
                DataCadastro = DateTime.UtcNow
            };

            _context.Turistas.Add(turista);
            await _context.SaveChangesAsync();

            return turista;
        }

        public async Task<List<Turista>> ListarTuristas()
        {
            return await _context.Turistas.ToListAsync();
        }

        public async Task<Turista?> DetalharTurista(int id)
        {
            return await _context.Turistas.FindAsync(id);
        }
    }
}
