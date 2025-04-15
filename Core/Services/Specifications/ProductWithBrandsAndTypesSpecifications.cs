using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) :base(P => P.Id ==id)
        {


            ApplyIncludes();
        }
        public ProductWithBrandsAndTypesSpecifications() : base(null)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
