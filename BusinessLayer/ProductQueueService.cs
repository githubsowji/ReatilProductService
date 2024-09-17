
using Microsoft.EntityFrameworkCore;
using RetailProductService.Data;
using RetailProductService.Interfaces;
using RetailProductService.Models;

namespace RetailProductService.Services {

    public class ProductQueueService : IQueueRepository {

        private readonly RetailProductDbContext _context;

        public ProductQueueService(RetailProductDbContext context) {
            _context=context;
        }

        public async Task AddToActionQueue(ProductQueueRequest productQueueRequest)
        {

            var createRequest= new ProductQueue{
                Id= productQueueRequest.Id,
                ProductId= productQueueRequest.ProductId,
                RequestReason= productQueueRequest.Reason,
                QueueAction= productQueueRequest.QueueAction,
                CreatedDate= DateTime.Now
            };
            _context.ProductQueues.Add(createRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductQueue>> GetProductsForAction(){

            return await _context.ProductQueues.Where(a=>a.IsApproved==false).OrderBy(a=>a.CreatedDate).ToListAsync();
        }

        public async Task ApproveProductRequest(ProductQueueRequest productQueueRequest){

            var existingQueueRequest = await _context.ProductQueues.FindAsync(productQueueRequest.Id);
            if(existingQueueRequest == null) {
                throw new Exception("There is no request in Queue");

            }
            existingQueueRequest.IsApproved= true;

            var existingProduct= await _context.ProductCatalogs.FindAsync(productQueueRequest.ProductId);
            if(existingProduct==null){
                throw new Exception("Product Not found");
            }
            if(existingProduct.ProductAction=="Delete"){

                _context.ProductCatalogs.Remove(existingProduct);
            }
            _context.ProductQueues.Remove(existingQueueRequest);

            await _context.SaveChangesAsync();
        }

         public async Task RejectProductRequest(ProductQueueRequest productQueueRequest){

            var existingQueueRequest = await _context.ProductQueues.FindAsync(productQueueRequest.Id);
            if(existingQueueRequest == null) {
                throw new Exception("There is no request in Queue");

            }
            existingQueueRequest.IsApproved= false;

            var existingProduct= await _context.ProductCatalogs.FindAsync(productQueueRequest.ProductId);
            if(existingProduct == null) {
                throw new Exception("Product Not found");
            }
            if(existingProduct.ProductAction=="Delete"){

                _context.ProductCatalogs.Remove(existingProduct);
            }
            await _context.SaveChangesAsync();
        }

    }
}