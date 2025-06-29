﻿using AutoMapper;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Extensions;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Parameters;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Queries.GetAllProducts;

public class GetAllProductsQuery : RequestParameter, IRequest<PaginatedResponse<List<ProductViewDto>>>
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<List<ProductViewDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
      

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<List<ProductViewDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _productRepository.GetAllAsync();
            products.Paginated(request.PageNumber, request.PageSize, out int totalItems, out var paginatedDatas);
            List<ProductViewDto> productsViewModel = _mapper.Map<List<ProductViewDto>>(paginatedDatas);

            return PaginatedResponse<List<ProductViewDto>>.SuccessWithMessage(productsViewModel, "Başarılı", totalItems, request.PageNumber, request.PageSize);
        }
    }
}
