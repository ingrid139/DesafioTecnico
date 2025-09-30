namespace Intituicao.Financeira.Application.Shared.Core
{
    public interface IRequest<T> : IRequest
    {
    }

    public interface IRequest
    {
        Guid CorrelationId { get; set; }
    }
}
