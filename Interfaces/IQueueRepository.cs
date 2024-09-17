using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductService.Models;

namespace RetailProductService.Interfaces 
{
    
public interface IQueueRepository{

    Task AddToActionQueue(ProductQueueRequest productQueueRequest);
   Task<List<ProductQueue>> GetProductsForAction();
    Task ApproveProductRequest(ProductQueueRequest productQueueRequest);
    Task RejectProductRequest(ProductQueueRequest productQueueRequest);
}
}