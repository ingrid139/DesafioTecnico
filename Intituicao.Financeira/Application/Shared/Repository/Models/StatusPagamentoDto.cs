using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intituicao.Financeira.Application.Shared.Repository.Models
{
    [Table("StatusPagamento")]
    public class StatusPagamentoDto
    {
        [Column("Id")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Column("Descricao")]
        [StringLength(100)]
        [Required]
        public string Descricao { get; set; }

        public PagamentoDto Pagamento { get; set; }
    }
}
