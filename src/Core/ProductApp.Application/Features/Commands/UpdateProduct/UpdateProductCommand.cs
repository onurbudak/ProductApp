using AutoMapper;
using MassTransit;
using MediatR;
using ProductApp.Application.Common;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ServiceResponse<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
    public short Status { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<long>>
    {

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task<ServiceResponse<long>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            var response = await productRepository.UpdateAsync(product);

            await publishEndpoint.Publish(new ProductMessage { Id = product.Id, Status = 35 });

            return ServiceResponse<long>.SuccessDataWithMessage(response.Id, "Başarılı");
        }
    }
}
