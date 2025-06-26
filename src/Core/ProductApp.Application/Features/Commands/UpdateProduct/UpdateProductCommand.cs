using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Middlewares;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ServiceResponse<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<long>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
        public async Task<ServiceResponse<long>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateProductCommandHandler method started");
            var product = _mapper.Map<Product>(request);
            var response = await _productRepository.UpdateAsync(product);

            await _publishEndpoint.Publish(new ProductMessage { Id = product.Id, Status = 101 }, cancellationToken);

            return ServiceResponse<long>.SuccessDataWithMessage(response.Id, "Başarılı");
        }
    }
}
