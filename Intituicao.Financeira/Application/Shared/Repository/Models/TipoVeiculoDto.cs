using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intituicao.Financeira.Application.Shared.Repository.Models
{
    [Table("TipoVeiculo")]
    public class TipoVeiculoDto
    {
        [Column("Id")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Column("Descricao")]
        [StringLength(100)]
        [Required]
        public string Descricao { get; set; }

        public ContratoDto Contrato { get; set; }
    }
}