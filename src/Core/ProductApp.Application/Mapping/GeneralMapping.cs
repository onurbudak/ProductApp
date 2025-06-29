﻿using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Commands.CreateProduct;
using ProductApp.Application.Features.Commands.DeleteProduct;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Product, ProductViewDto>()
            .ReverseMap();
        CreateMap<Product, CreateProductCommand>()
            .ReverseMap();
        CreateMap<Product, UpdateProductCommand>()
         .ReverseMap();
        CreateMap<Product, DeleteProductCommand>()
         .ReverseMap();
        CreateMap<ProductMessage, Product>()
           .ReverseMap();
    }
}
