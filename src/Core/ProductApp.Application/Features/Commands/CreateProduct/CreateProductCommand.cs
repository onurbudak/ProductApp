using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ServiceResponse<long>>
{
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<long>>
    {

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product);

            return ServiceResponse<long>.SuccessDataWithMessage(product.Id, "Başarılı");
        }
    }
}
