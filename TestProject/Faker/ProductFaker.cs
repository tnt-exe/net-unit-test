using Bogus;
using WebApi.Models;

namespace TestProject.Faker;
public static class ProductFaker
{
    private static readonly Faker<Product> _productFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.IndexFaker + 1)
        .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription());

    public static List<Product> GenerateFakeProductList(int count)
        => _productFaker.Generate(count);

    public static Product GenerateFakeProduct()
        => _productFaker.Generate();
}
