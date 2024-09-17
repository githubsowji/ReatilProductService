using System.ComponentModel.DataAnnotations;

namespace RetailProductService.Models{

    public class ProductQueueRequest
    {

public Guid Id {get;set;}
[Required]
     public Guid ProductId { get;set;}
[Required]
     public bool IsApproved {get;set;}

     public string Reason {get;set;}

     public string QueueAction {get;set;}
    }
}