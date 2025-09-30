using Intituicao.Financeira.Application.Shared.Repository.Interfaces;
using Intituicao.Financeira.Application.Shared.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Intituicao.Financeira.Application.Shared.Repository
{
    public class ContractRepository(InstituicaoContexto contexto) : IContractRepository
    {
        private InstituicaoContexto _contexto = contexto;

        public ContratoDto GetById(Guid id)
        {
            return _contexto.Contratos.Find(id);
        }

        public IEnumerable<ContratoDto> GetByClient(long cpfCnpj)
        {
            return _contexto.Contratos
                            .Where(x => x.ClienteCpfCnpj == cpfCnpj)
                            .Include(p => p.Pagamentos)
                            .ToList();
        }

        public bool DeleteById(Guid id)
        {
            var contrato = _contexto.Contratos.Find(id);

            if (contrato is null) return false;

            _contexto.Contratos.Remove(contrato);
            _contexto.SaveChanges();

            return true;
        }

        public ContratoDto Insert(ContratoDto contrato)
        {
            _contexto.Contratos.Add(contrato);
            _contexto.SaveChanges();

            return contrato;
        }
    }
}
