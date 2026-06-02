using System.ComponentModel.DataAnnotations;

namespace SpaceCare.Domain.Turistas.Dtos
{
    public class CadastroTuristaDto
    {
        [Required(ErrorMessage = "O nome do turista é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número do passaporte espacial é obrigatório.")]
        [StringLength(20, ErrorMessage = "O passaporte espacial não pode passar de {1} caracteres.")]
        public string PassaporteEspacial { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail digitado é inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail não pode passar de {1} caracteres.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "O histórico médico pode ter no máximo {1} caracteres.")]
        public string? HistoricoMedico { get; set; }
    }
}
