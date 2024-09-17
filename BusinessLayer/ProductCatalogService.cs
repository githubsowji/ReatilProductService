using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RetailProductService.Data;
using RetailProductService.Interfaces;
using RetailProductService.Models;

namespace RetailProductService.Services {

    public class ProductCatalogService : IProductCatalogRepository  {

        private readonly RetailProductDbContext _context;
        private readonly ProductQueueService  _productQueueService;

        public ProductCatalogService(RetailProductDbContext context, ProductQueueService productQueueService){

            _context= context;
            _productQueueService=productQueueService;
        }

        public async Task<List<ProductCatalog>> SearchProducts(ProductSearchRequest searchRequest)
        {

            var query = await  _context.ProductCatalogs.Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();
            if (!string.IsNullOrEmpty(searchRequest.Name))
            {
                query = query.Where(p => p.Name.Contains(searchRequest.Name)).ToList();
            }
            if (searchRequest.PriceMin != null && searchRequest.PriceMax != null)
            {
                query = (List<ProductCatalog>)query.Where(p => p.MSRP > searchRequest.PriceMin && p.MSRP < searchRequest.PriceMax);

            }
            if (searchRequest.CreatedDateStart != null && searchRequest.CreatedDateEnd != null)
            {
                query = (List<ProductCatalog>)query.Where(p => p.CreatedDate > searchRequest.CreatedDateStart && p.CreatedDate < searchRequest.CreatedDateEnd);

            }

            return query;

        }

        public async Task<ProductCatalog> CreateProduct(ProductRequest productRequest)
        {

            // if(!ModelState.IsValid){

            //     return BadRequest(ModelState);
            // }

            if(productRequest.Price > 10000)
            throw new Exception("Product price exceeds the allowed limit");
            var product = new ProductCatalog{
                Id= Guid.NewGuid(),
                Name=productRequest.Name,
                MSRP=productRequest.Price,
                ProductAction=productRequest.ProductAction,
                CreatedDate=DateTime.Now,
                IsActive= true
            };
            _context.ProductCatalogs.Add(product);
            await _context.SaveChangesAsync();
        var queueRequest= new ProductQueueRequest{
                Id= Guid.NewGuid(),
                ProductId= productRequest.Id,
                Reason ="Price Greater Than 5000 ",
                QueueAction ="Approva1"
            };
            if(productRequest.Price> 5000){
                await _productQueueService.AddToActionQueue(queueRequest);
            }

            return product;
        }
   
        public async Task<ProductCatalog> UpdateProduct(ProductRequest productRequest){

            var existingProduct= await _context.ProductCatalogs.FindAsync(productRequest.Id);
            if(existingProduct==null)
            {
                throw new Exception("Product does not exist");

            }
          var previousPrice= existingProduct.MSRP*1.5m;
            existingProduct.Name= productRequest.Name;
            existingProduct.MSRP=productRequest.Price;
            existingProduct.ProductAction= productRequest.ProductAction;
            existingProduct.UpdatedDate= DateTime.Now;

            _context.ProductCatalogs.Update(existingProduct);
            await _context.SaveChangesAsync();

            var queueRequest= new ProductQueueRequest{
                Id= Guid.NewGuid(),
                ProductId= productRequest.Id,
                Reason ="Price Greater Than 5000 or Greater than Previous Price more than 1.5 times",
                QueueAction ="Approva1"
            };
            if(existingProduct.MSRP > 5000 || existingProduct.MSRP> previousPrice)
            {
               await  _productQueueService.AddToActionQueue(queueRequest);
            }

            return existingProduct;
        }

        public async Task DeleteProduct(Guid id){

            var existingProduct= await _context.ProductCatalogs.FindAsync(id);
            if(existingProduct==null)
            {
                throw new Exception("Product does not exist");

            }
            existingProduct.ProductAction= "Delete";

         var queueRequest= new ProductQueueRequest{
                Id= Guid.NewGuid(),
                ProductId= existingProduct.Id,
                Reason ="Product not in use ",
                QueueAction ="Approva1"
            };
            await _productQueueService.AddToActionQueue(queueRequest);
        }
    }
}