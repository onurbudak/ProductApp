using AutoMapper;
using MassTransit;
using ProductApp.Application.Interfaces.Repositories;
using ProductApp.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MassTransit.Metadata;
using ProductApp.Application.Events;

namespace ProductApp.Application.Consumers;

public class UpdateProductConsumer : IConsumer<UpdateProductEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductConsumer> _logger;

    public UpdateProductConsumer(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductConsumer> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<UpdateProductEvent> context)
    {
        try
        {
            Console.WriteLine($"UpdateProductConsumer Started");
            _logger.LogInformation($"UpdateProductConsumer Started");

            UpdateProductEvent updateProductEvent = context.Message;

            Console.WriteLine($"UpdateProductConsumer received Status : {updateProductEvent.Status}");
            _logger.LogInformation("UpdateProductConsumer received Status : {Status}", updateProductEvent.Status);

            Stopwatch timer = Stopwatch.StartNew();

            var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
            updateProductEvent.Status = Convert.ToInt16(status);

            updateProductEvent.Status += 1;

            Product? mappedProduct = _mapper.Map<Product>(updateProductEvent);
            await _productRepository.UpdateAsync(mappedProduct);
            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<Product>.ShortName);
            timer.Stop();

            Console.WriteLine($"UpdateProductConsumer Finished");
            _logger.LogInformation($"UpdateProductConsumer Finished");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error UpdateProductConsumer : {ex}");
            _logger.LogError(ex, "Error UpdateProductConsumer");
            throw;
        }
    }
}
