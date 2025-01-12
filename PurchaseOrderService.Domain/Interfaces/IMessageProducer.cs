
namespace PurchaseOrderService.Domain.Interfaces
{
    public interface IMessageProducer
    {
        Task SendMessageAsync<T>(T message);
    }
}
