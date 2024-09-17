
using Microsoft.AspNetCore.Mvc;
using RetailProductService.Models;
using RetailProductService.Services;

[ApiController]
[Route("api/ProductQueue")]
public class ProductQueueController :ControllerBase{

    private readonly ProductQueueService _productQueueService;

    public ProductQueueController(ProductQueueService productQueueService){
        _productQueueService=productQueueService;
    }

    [HttpPost("{id}/performAction")]
    public async Task<IActionResult> PerformAction([FromBody] ProductQueueRequest productQueueRequest){

 if(!ModelState.IsValid){

              return BadRequest(ModelState);
             }
      
      if(productQueueRequest.IsApproved){
        await _productQueueService.ApproveProductRequest(productQueueRequest);
      }
      else
      await _productQueueService.RejectProductRequest(productQueueRequest);

      return NoContent();
    }

     [HttpGet]
    public async Task<IActionResult> GetProductsForReview(){

        var productsToReview= await _productQueueService.GetProductsForAction();
        return Ok(productsToReview);
    }


  

    
}