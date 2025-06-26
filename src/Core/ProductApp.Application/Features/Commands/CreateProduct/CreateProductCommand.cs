using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ServiceResponse<long>>
{
    public string? Name { get; set; }
    public decimal? Value { get; set; }
    public long? Quantity { get; set; }
    public short Status { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<long>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);

            return ServiceResponse<long>.SuccessDataWithMessage(product.Id, "Başarılı");
        }
    }
}
