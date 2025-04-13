﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            // Get all products 
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            // Map to DTO

            var productDtos = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return productDtos;
        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);
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
