using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParamters SpecParams)
        {

            var spec = new ProductWithBrandsAndTypesSpecifications(SpecParams);
            
            // Get all products 
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            // Map to DTO
            var specCount = new ProductWithBrandsAndTypesSpecifications(SpecParams);
            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);

            var productDtos = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginationResponse<ProductResultDto>(SpecParams.PageIndex,SpecParams.PageSize,count, productDtos);
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            // Map to DTO
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var productDto = mapper.Map<ProductResultDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            // Get all brands

            // Map to DTO
            var brandDtos = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandDtos;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            // Get all brands

            // Map to DTO
            var typesDtos = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesDtos;
        }


    }
}
