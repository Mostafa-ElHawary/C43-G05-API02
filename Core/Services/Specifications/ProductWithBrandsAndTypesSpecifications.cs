using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {


            ApplyIncludes();
        } 
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParamters SpecParams) : base(

            P => 
            (string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search.ToLower()))&&
            (!SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId) &&
            (!SpecParams.TypeId.HasValue || P.TypeId == SpecParams.TypeId)

            )
        {
            ApplyIncludes();

            ApplySorting(SpecParams.Sort);

            ApplyPagination(SpecParams.PageIndex, SpecParams.PageSize);
        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {

                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

        }
    }
}
