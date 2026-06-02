using System.ComponentModel.DataAnnotations;

namespace SpaceCare.Domain.Turistas.Dtos
{
    public class AtualizarTurista
    {
        [Required(ErrorMessage = "O ID do turista é obrigatório para realizar a atualização.")]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O nome não pode passar de {1} caracteres.")]
        public string? Nome { get; set; }

        [StringLength(20, ErrorMessage = "O passaporte espacial não pode passar de {1} caracteres.")]
        public string? PassaporteEspacial { get; set; }

        [EmailAddress(ErrorMessage = "O formato do e-mail digitado é inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail não pode passar de {1} caracteres.")]
        public string? Email { get; set; }

        [StringLength(250, ErrorMessage = "O histórico médico pode ter no máximo {1} caracteres.")]
        public string? HistoricoMedico { get; set; }
    }
}
