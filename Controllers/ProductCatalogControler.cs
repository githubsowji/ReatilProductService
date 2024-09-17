
using Microsoft.AspNetCore.Mvc;
using RetailProductService.Models;
using RetailProductService.Services;

[ApiController]
[Route("api/RetailProducts")]
public class ProductCatalogController :ControllerBase{

    private readonly ProductCatalogService _productCatalogService;

    public ProductCatalogController(ProductCatalogService productCatalogService){
        _productCatalogService=productCatalogService;
    }

    [HttpGet]
    public async Task<IActionResult> SearchProducts([FromQuery] ProductSearchRequest productSearchRequest){

        var products= await _productCatalogService.SearchProducts(productSearchRequest);
        return Ok(products);
    }

    [HttpPost]

    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest){
 
           if(!ModelState.IsValid){

              return BadRequest(ModelState);
             }
        var createdProduct= await _productCatalogService.CreateProduct(productRequest);
        return CreatedAtAction("Created",createdProduct);
    }

    [HttpPost]

    public async Task<IActionResult> UpdateProduct([FromBody] ProductRequest productRequest){

            if(!ModelState.IsValid){

              return BadRequest(ModelState);
             }
        await _productCatalogService.UpdateProduct(productRequest);
        return NoContent();
    }


    [HttpGet]

    public async Task<IActionResult> DeleteProduct([FromQuery] Guid id){

        await _productCatalogService.DeleteProduct(id);
        return NoContent();
    }
}