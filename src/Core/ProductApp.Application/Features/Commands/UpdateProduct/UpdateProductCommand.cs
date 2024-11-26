using AutoMapper;
using MediatR;
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

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<long>>
    {

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<long>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            var response = await productRepository.UpdateAsync(product);

            return ServiceResponse<long>.SuccessDataWithMessage(response.Id, "Başarılı");
        }
    }
}
