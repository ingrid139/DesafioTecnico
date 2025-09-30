using Intituicao.Financeira.Application.Shared.Repository.Models;

namespace Intituicao.Financeira.Application.Shared.Repository.Interfaces
{
    public interface IContractRepository
    {
        bool DeleteById(Guid id);
        IEnumerable<ContratoDto> GetByClient(long cpfCnpj);
        ContratoDto GetById(Guid id);
        ContratoDto Insert(ContratoDto contrato);
    }
}