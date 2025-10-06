using ProductApp.Domain.Common;

namespace ProductApp.Domain.Entities;

public class OperationClaim : BaseEntity<long>
{
    public required string Name { get; set; }

}