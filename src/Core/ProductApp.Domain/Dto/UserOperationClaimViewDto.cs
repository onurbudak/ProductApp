using ProductApp.Domain.Common;

namespace ProductApp.Domain.Dto;

public class UserOperationClaimViewDto : IDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long OperationClaimId { get; set; }
}
