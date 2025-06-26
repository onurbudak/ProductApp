using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Application.Common;
using Microsoft.Extensions.Logging;

namespace ProductApp.Application.Consumers;

public class ProductMessageConsumer : IConsumer<ProductMessage>
{
    private readonly ILogger<ProductMessageConsumer> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductMessageConsumer(IProductRepository productRepository, IMapper mapper, ILogger<ProductMessageConsumer> logger)
    {
        _logger = logger;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        try
        {
            Console.WriteLine($"Consumer received message: {context.Message.Status}");
            _logger.LogInformation("Consumer received message: {Status}", context.Message.Status);
            var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
            context.Message.Status = Convert.ToInt16(status);
            var product = _mapper.Map<Product>(context.Message);
            await _productRepository.UpdateAsync(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error ProductMessageConsumer");
            throw new Exception(ex.Message, ex);
        }
    }
}
