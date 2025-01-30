using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Application.Common;

namespace ProductApp.Application.Consumers;

public class ProductMessageConsumer : IConsumer<ProductMessage>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public ProductMessageConsumer(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }
    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        try
        {
            Console.WriteLine($"Consumer received message: {context.Message.Status}");
            context.Message.Status = 1001;
            var product = mapper.Map<Product>(context.Message);
            await productRepository.UpdateAsync(null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
