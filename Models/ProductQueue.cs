namespace RetailProductService.Models
{
public class ProductQueue {

    public Guid Id {get; set;}

    public Guid ProductId {get; set;}

    public ProductCatalog ProductCatalog {get;set;}

    public bool IsApproved  {get; set;}

    public string QueueAction {get; set;}

    public string RequestReason {get;set;}

    public DateTime CreatedDate {get;set;}
}
}