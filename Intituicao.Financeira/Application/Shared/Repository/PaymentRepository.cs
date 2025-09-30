using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Intituicao.Financeira.Application.Shared.Repository
{
    public class PaymentRepository(InstituicaoContexto contexto) : IPaymentRepository
    {
        private InstituicaoContexto _contexto = contexto;

        public IEnumerable<PagamentoDto> GetByContract(Guid id)
        {
            return _contexto.Contratos
                .Where(x => x.Id == id)
                .SelectMany(x => x.Pagamentos)
                .Distinct()
                .OrderBy(x => x.DataVencimento)
                .ToList();
        }

        public IEnumerable<PagamentoDto> GetByClient(long cpfCnpj)
        {
            return _contexto.Contratos
                .Where(x => x.ClienteCpfCnpj == cpfCnpj)
                .SelectMany(x => x.Pagamentos)
                .Distinct()
                .ToList();
        }

        public PagamentoDto Insert(PagamentoDto pagamento)
        {
            _contexto.Pagamentos.Add(pagamento);
            _contexto.SaveChanges();

            return pagamento;
        }
    }
}
