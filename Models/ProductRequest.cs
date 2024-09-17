using System.ComponentModel.DataAnnotations;

namespace RetailProductService.Models
{

public class ProductRequest {

[Required]       
public Guid Id {get;set;}

[Required]
[MaxLength(250)]
 public string Name {get; set;}

[Required]
[Range(0,10000,ErrorMessage ="Price must be between 0 and 10000 dollars.")]
public decimal Price {get; set;}

[Required]
public string ProductAction {get; set;}



    }
}