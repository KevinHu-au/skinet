using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpec : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpec(ProductSpecParams specParams)
            : base(x => (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
                        (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
                        (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderby(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (specParams == null)
                return;

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderby(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderbyDescneding(p => p.Price);
                        break;
                    default:
                        AddOrderby(p => p.Name);
                        break;
                }
            }
        }

        public ProductWithTypeAndBrandSpec(int id) :
                                        base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}