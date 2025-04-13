using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync();

        Task<ProductResultDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();


    }
}
