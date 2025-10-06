using ProductApp.Domain.Common;

namespace ProductApp.Domain.Dto;

public class OperationClaimViewDto : IDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
}
