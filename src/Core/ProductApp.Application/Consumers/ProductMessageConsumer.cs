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
        Console.WriteLine($"Consumer received message: {context.Message.Status}");

        var product = mapper.Map<Product>(context.Message);
        await productRepository.UpdateAsync(product);

    }
}
