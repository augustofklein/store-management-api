using System.Collections.Generic;

namespace StoreManagement.Application.Product.Model
{
    public class ProductPagedResultDto
    {
        public IEnumerable<ProductDto> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
