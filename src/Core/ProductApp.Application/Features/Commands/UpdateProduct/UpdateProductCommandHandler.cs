using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse>
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
    public async Task<BaseResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("UpdateProductCommandHandler method started");

        Product mappedProduct = _mapper.Map<Product>(request);
        Product? product = await _productRepository.UpdateAsync(mappedProduct);

        //await _publishEndpoint.Publish(new ProductMessage { Id = product.Id, Status = 101 }, cancellationToken);

        return product is not null ? BaseResponse.SuccessMessage("Başarılı") : BaseResponse.ErrorMessage("Başarısız");
    }
}

