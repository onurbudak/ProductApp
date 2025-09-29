using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Commands.Products.CreateProduct;
using ProductApp.Application.Features.Commands.Products.DeleteProduct;
using ProductApp.Application.Features.Commands.Products.UpdateProduct;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Product, ProductViewDto>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<DeleteProductCommand, Product>();
        CreateMap<ProductMessage, Product>();
    }
}
