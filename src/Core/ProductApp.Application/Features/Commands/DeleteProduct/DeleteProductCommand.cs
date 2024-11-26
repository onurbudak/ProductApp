using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ServiceResponse<bool>>
{
    public long Id { get; set; }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<bool>>
    {

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            var response = await productRepository.DeleteAsync(product);

            return ServiceResponse<bool>.SuccessDataWithMessage(true, "Başarılı");
        }
    }
}
