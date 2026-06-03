using SpaceCare.Domain.Turistas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceCare.Domain.Telemetrias
{
    [Table("SC_TELEMETRIAS")]
    public class Telemetria
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TURISTA_ID")]
        public int TuristaId { get; set; }

        [Required]
        [Column("BATIMENTOS")]
        public int BatimentosCardiacos { get; set; }

        [Required]
        [Column("TEMPERATURA", TypeName = "NUMBER(4,2)")]
        public decimal TemperaturaCorporal { get; set; }

        [Required]
        [StringLength(10)]
        [Column("PRESSAO_ARTERIAL")]
        public string PressaoArterial { get; set; } = string.Empty;

        [Required]
        [Column("DT_LEITURA")]
        public DateTime DataLeitura { get; set; } = DateTime.UtcNow;

        [ForeignKey("TuristaId")]
        public virtual Turista? Turista { get; set; }
    }
}
