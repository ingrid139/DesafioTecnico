using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intituicao.Financeira.Application.Shared.Repository.Models
{
    [Table("Pagamento")]
    public class PagamentoDto
    {
        [Column("Id")]
        [Required]
        [Key]
        public Guid Id { get; set; }


        [Column("DataVencimento")]
        [Required]
        public DateTime DataVencimento { get; set; }

        [Column("DataPagamento")]
        [Required]
        public DateTime DataPagamento { get; set; }


        [Column("SaldoDevedor", TypeName = "decimal(9,2)")]
        [Required]
        public decimal SaldoDevedor { get; set; }

        public int StatusPagamentoId { get; set; }

        public Guid ContratoId { get; set; }

    }
}
