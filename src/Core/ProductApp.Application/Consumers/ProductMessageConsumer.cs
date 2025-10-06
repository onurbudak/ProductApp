using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MassTransit.Metadata;
using ProductApp.Application.Events;

namespace ProductApp.Application.Consumers;

public class ProductMessageConsumer : IConsumer<UpdateProductEvent>
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
    public async Task Consume(ConsumeContext<UpdateProductEvent> context)
    {
        try
        {
            Console.WriteLine($"ProductMessageConsumer Started");
            _logger.LogInformation($"ProductMessageConsumer Started");

            UpdateProductEvent updateProductEvent = context.Message;

            Console.WriteLine($"ProductMessageConsumer received Status : {updateProductEvent.Status}");
            _logger.LogInformation("ProductMessageConsumer received Status : {Status}", updateProductEvent.Status);

            Stopwatch timer = Stopwatch.StartNew();

            var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
            updateProductEvent.Status = Convert.ToInt16(status);

            updateProductEvent.Status += 1;

            Product? mappedProduct = _mapper.Map<Product>(updateProductEvent);
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
            throw;
        }
    }
}
