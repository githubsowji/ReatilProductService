using System.ComponentModel.DataAnnotations;

namespace RetailProductService.Models{

    public class ProductSearchRequest {

        public string  Name {get;set;}

        [Range(0,10000,ErrorMessage ="Price must be greater than 0.")]
        public decimal PriceMin {get;set;}

        [Range(0,10000,ErrorMessage ="Price must be greater than 0.")]
        public decimal PriceMax {get;set;}

        public DateTime? CreatedDateStart {get;set;}

        public DateTime? CreatedDateEnd {get; set;}
    }
}