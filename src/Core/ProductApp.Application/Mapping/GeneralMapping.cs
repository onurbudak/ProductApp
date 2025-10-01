using AutoMapper;
using ProductApp.Application.Common;
using ProductApp.Application.Features.Commands.OperationClaims.CreateOperationClaim;
using ProductApp.Application.Features.Commands.OperationClaims.DeleteOperationClaim;
using ProductApp.Application.Features.Commands.OperationClaims.UpdateOperationClaim;
using ProductApp.Application.Features.Commands.Products.CreateProduct;
using ProductApp.Application.Features.Commands.Products.DeleteProduct;
using ProductApp.Application.Features.Commands.Products.UpdateProduct;
using ProductApp.Application.Features.Commands.RefreshTokens.CreateRefreshToken;
using ProductApp.Application.Features.Commands.RefreshTokens.DeleteRefreshToken;
using ProductApp.Application.Features.Commands.RefreshTokens.UpdateRefreshToken;
using ProductApp.Application.Features.Commands.UserOperationClaims.CreateUserOperationClaim;
using ProductApp.Application.Features.Commands.UserOperationClaims.DeleteUserOperationClaim;
using ProductApp.Application.Features.Commands.UserOperationClaims.UpdateUserOperationClaim;
using ProductApp.Application.Features.Commands.Users.CreateUser;
using ProductApp.Application.Features.Commands.Users.DeleteUser;
using ProductApp.Application.Features.Commands.Users.UpdateUser;
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
        CreateMap<ProductMessage, Product>();

        CreateMap<User, UserViewDto>();
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
