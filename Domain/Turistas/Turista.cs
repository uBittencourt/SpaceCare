using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceCare.Domain.Turistas
{
    [Table("SC_TURISTAS")]
    public class Turista
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("NR_PASSAPORTE_ESPACIAL")]
        public string PassaporteEspacial { get; set; } = string.Empty;

        [Required]
        [Column("DT_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(150)]
        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [StringLength(250)]
        [Column("HISTORICO_MEDICO")]
        public string? HistoricoMedico { get; set; }

        [Required]
        [Column("DT_CADASTRO")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("ATIVO")]
        public string Ativo { get; set; } = "1";
    }
}
