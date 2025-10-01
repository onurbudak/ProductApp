using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repositories;
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
            Console.WriteLine($"ProductMessageConsumer Started");
            _logger.LogInformation($"ProductMessageConsumer Started");

            ProductMessage productMessage = context.Message;

            Console.WriteLine($"ProductMessageConsumer received Status : {productMessage.Status}");
            _logger.LogInformation("ProductMessageConsumer received Status : {Status}", productMessage.Status);

            Stopwatch timer = Stopwatch.StartNew();

            var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
            productMessage.Status = Convert.ToInt16(status);

            productMessage.Status += 1;

            Product? mappedProduct = _mapper.Map<Product>(productMessage);
            await _productRepository.UpdateAsync(mappedProduct);
            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<Product>.ShortName);
            timer.Stop();

            Console.WriteLine($"ProductMessageConsumer Finished");
            _logger.LogInformation($"ProductMessageConsumer Finished");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductMessageConsumer Error : {ex}");
            _logger.LogError(ex, "ProductMessageConsumer Error");
            throw new Exception(ex.Message, ex);
        }
    }
}
