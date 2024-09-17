
namespace RetailProductService.Models{


public class ProductCatalog
{
    public Guid Id {get;set;}

    public required string Name {get; set;}

    public string? Description {get; set;}
    public decimal MSRP {get;set;}

     public decimal SalePrice {get;set;}
    public bool IsActive {get;set;}

    public DateTime CreatedDate {get;set;}

    public  string? CreatedBy {get; set;}

    public DateTime UpdatedDate {get; set;}

    public  string? UpdatedBy {get;set;}

    public required string ProductAction {get; set;}
}
}