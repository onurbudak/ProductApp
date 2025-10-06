namespace ProductApp.Application.Filters;

public class FilterCriteria
{
    public required string Field { get; set; }     
    public required string Operator { get; set; }  
    public required object Value { get; set; }   
}

