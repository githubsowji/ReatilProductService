using System.Collections.Generic;
using System.Threading.Tasks;
using RetailProductService.Models;

namespace RetailProductService.Interfaces 
{
    
public interface IProductCatalogRepository{

    Task<List<ProductCatalog>> SearchProducts(ProductSearchRequest search);
    Task<ProductCatalog> CreateProduct(ProductRequest productRequest);
    Task<ProductCatalog> UpdateProduct(ProductRequest productRequest);
    Task DeleteProduct(Guid id);
}
}