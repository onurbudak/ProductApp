using AutoMapper;
using ProductApp.Application.Events;
using ProductApp.Application.Features.OperationClaims.CreateOperationClaim;
using ProductApp.Application.Features.OperationClaims.DeleteOperationClaim;
using ProductApp.Application.Features.OperationClaims.UpdateOperationClaim;
using ProductApp.Application.Features.Products.CreateProduct;
using ProductApp.Application.Features.Products.DeleteProduct;
using ProductApp.Application.Features.Products.UpdateProduct;
using ProductApp.Application.Features.RefreshTokens.CreateRefreshToken;
using ProductApp.Application.Features.RefreshTokens.DeleteRefreshToken;
using ProductApp.Application.Features.RefreshTokens.UpdateRefreshToken;
using ProductApp.Application.Features.UserOperationClaims.CreateUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.DeleteUserOperationClaim;
using ProductApp.Application.Features.UserOperationClaims.UpdateUserOperationClaim;
using ProductApp.Application.Features.Users.CreateUser;
using ProductApp.Application.Features.Users.DeleteUser;
using ProductApp.Application.Features.Users.UpdateUser;
using ProductApp.Domain.Dto;
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
        CreateMap<UpdateProductEvent, Product>();

        CreateMap<User, UserViewDto>().ReverseMap();
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<DeleteUserCommand, User>();

        CreateMap<UserOperationClaim, UserOperationClaimViewDto>();
        CreateMap<CreateUserOperationClaimCommand, UserOperationClaim>();
        CreateMap<UpdateUserOperationClaimCommand, UserOperationClaim>();
        CreateMap<DeleteUserOperationClaimCommand, UserOperationClaim>();

        CreateMap<OperationClaim, OperationClaimViewDto>();
        CreateMap<CreateOperationClaimCommand, OperationClaim>();
        CreateMap<UpdateOperationClaimCommand, OperationClaim>();
        CreateMap<DeleteOperationClaimCommand, OperationClaim>();

        CreateMap<RefreshToken, RefreshTokenViewDto>();
        CreateMap<CreateRefreshTokenCommand, RefreshToken>();
        CreateMap<UpdateRefreshTokenCommand, RefreshToken>();
        CreateMap<DeleteRefreshTokenCommand, RefreshToken>();
    }
}
