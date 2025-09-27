using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Entities;
using ProductApp.Application.Common;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MassTransit.Metadata;

namespace ProductApp.Application.Consumers;

public class ProductMessageConsumer : IConsumer<ProductMessage>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductMessageConsumer> _logger;

    public ProductMessageConsumer(IProductRepository productRepository, IMapper mapper, ILogger<ProductMessageConsumer> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        try
        {
            Console.WriteLine($"ProductMessageConsumer received Status : {context.Message.Status}");
            _logger.LogInformation("ProductMessageConsumer received Status : {Status}", context.Message.Status);

            Stopwatch timer = Stopwatch.StartNew();        
            var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
            context.Message.Status = Convert.ToInt16(status);
            Product? mappedProduct = _mapper.Map<Product>(context.Message);
            await _productRepository.UpdateAsync(mappedProduct);
            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<Product>.ShortName);
            timer.Stop();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductMessageConsumer Error : {ex}");
            _logger.LogError(ex, "ProductMessageConsumer Error");
            throw new Exception(ex.Message, ex);
        }
    }
}
