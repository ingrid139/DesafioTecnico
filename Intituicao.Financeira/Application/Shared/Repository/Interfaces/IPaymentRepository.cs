using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Intituicao.Financeira.Application.Shared.Repository.Interfaces
{
    public interface IPaymentRepository
    {
        IEnumerable<PagamentoDto> GetByClient(long cpfCnpj);
        IEnumerable<PagamentoDto> GetByContract(Guid id);
        PagamentoDto Insert(PagamentoDto pagamento);
    }
}