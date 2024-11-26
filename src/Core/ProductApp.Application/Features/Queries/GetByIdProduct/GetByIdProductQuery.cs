using AutoMapper;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.GetByIdProduct;

public class GetByIdProductQuery : IRequest<ServiceResponse<ProductViewDto>>
{
    public long Id { get; set; }

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ServiceResponse<ProductViewDto>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<ProductViewDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetByIdAsync(request.Id);
            ProductViewDto productViewModel = mapper.Map<ProductViewDto>(product);

            return ServiceResponse<ProductViewDto>.SuccessDataWithMessage(productViewModel, "Başarılı");
        }
    }
}
