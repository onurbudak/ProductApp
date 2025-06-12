using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Application.Common;

namespace ProductApp.Application.Consumers;

public class ProductMessageConsumer : IConsumer<ProductMessage>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductMessageConsumer(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        try
        {
            Console.WriteLine($"Consumer received message: {context.Message.Status}");
            context.Message.Status = 1001;
            var product = _mapper.Map<Product>(context.Message);
            await _productRepository.UpdateAsync(null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
