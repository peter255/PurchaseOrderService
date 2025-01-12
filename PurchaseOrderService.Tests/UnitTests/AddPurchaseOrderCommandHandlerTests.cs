using Moq;
using FluentAssertions;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;
using PurchaseOrderService.Domain.ValueObjects;


namespace PurchaseOrderService.Tests.UnitTests
{
    public class AddPurchaseOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_PurchaseOrder()
        {
            // Arrange
            var mockRepository = new Mock<IPurchaseOrderRepository>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var handler = new CreatePurchaseOrderHandler(mockRepository.Object, mockMessageProducer.Object);
            var command = new CreatePurchaseOrderCommand("PO123410101", DateTime.Now,
                new List<PurchaseOrderItemDto>
                {
                    new PurchaseOrderItemDto("GC1234574575", new Money(50.00m)),
                    new PurchaseOrderItemDto("GC1234674111", new Money(150.00m)),
                });

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().Be(1); // Assuming the result is an integer representing the number of records added
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.PurchaseOrder>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_TotalPrice_Is_Negative()
        {
            // Arrange
            var mockRepository = new Mock<IPurchaseOrderRepository>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var handler = new CreatePurchaseOrderHandler(mockRepository.Object, mockMessageProducer.Object);
            var command = new CreatePurchaseOrderCommand("PO12345", DateTime.Now, new List<PurchaseOrderItemDto>
                {
                    new PurchaseOrderItemDto("GC1234574585", new Money(50.00m)),
                    new PurchaseOrderItemDto("GC1234678787", new Money(150.00m)),
                });

            // Act
            Func<Task> action = async () => await handler.Handle(command, default);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Total price cannot be negative.");
        }
    }
}

