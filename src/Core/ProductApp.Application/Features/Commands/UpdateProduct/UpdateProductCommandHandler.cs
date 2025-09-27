using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Messaging;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<ServiceResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductCommand Started");

        //var status = 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 9999 + 999999999999;
        //var newStatus = Convert.ToInt16(status);

        Product mappedProduct = _mapper.Map<Product>(request);
        Product? product = await _productRepository.UpdateAsync(mappedProduct);

        if (product is null)
        {
            return ServiceResponse<bool>.FailureDataWithMessage(Messages.RecordIsNotFound, new Error(MessageCode.RecordIsNotFound, Messages.RecordIsNotFound));
        }

        await _publishEndpoint.Publish(new ProductMessage { Id = product.Id, Status = 1 }, cancellationToken);

        return ServiceResponse<bool>.SuccessDataWithMessage(true, Messages.Success);
    }
}

