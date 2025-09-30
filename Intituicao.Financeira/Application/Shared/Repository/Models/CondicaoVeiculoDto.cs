using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intituicao.Financeira.Application.Shared.Repository.Models
{
    [Table("CondicaoVeiculo")]
    public class CondicaoVeiculoDto
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
