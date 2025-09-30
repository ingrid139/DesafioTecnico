using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intituicao.Financeira.Application.Shared.Repository.Models
{
    [Table("Contrato")]
    public class ContratoDto
    {
        [Column("Id")]
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Column("ClienteCpfCnpj")]
        [Required]
        public long ClienteCpfCnpj { get; set; }

        [Column("ValorTotal", TypeName = "decimal(9,2)")]
        [Required]
        public decimal ValorTotal { get; set; }

        [Column("TaxaMensal", TypeName = "decimal(2,2)")]
        [Required]
        public decimal TaxaMensal { get; set; }

        [Column("PrazoMeses")]
        [Required]
        public int PrazoMeses { get; set; }

        [Column("DataVencimentoPrimeiraParcela")]
        [Required]
        public DateTime DataVencimentoPrimeiraParcela { get; set; }

        public int TipoVeiculoId { get; set; }
        public int CondicaoVeiculoId { get; set; }

        public ICollection<PagamentoDto> Pagamentos { get; set; }
    }
}
