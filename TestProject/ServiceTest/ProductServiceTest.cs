using AwesomeAssertions;
using Moq;
using TestProject.Faker;
using WebApi.Models;
using WebApi.Repositories.Interfaces;
using WebApi.Services;

namespace TestProject.ServiceTest;

public class ProductServiceTest
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductService _productService;
    public ProductServiceTest()
    {
        _mockRepo = new Mock<IProductRepository>();
        _productService = new ProductService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnAllProducts()
    {
        var productCount = 5;
        var productList = ProductFaker.GenerateFakeProductList(productCount);
        _mockRepo.Setup(s => s.GetAllAsync()).ReturnsAsync(productList);

        var result = await _productService.GetAllProductsAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(productCount);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        var product = ProductFaker.GenerateFakeProduct();
        _mockRepo.Setup(s => s.GetByIdAsync(product.Id)).ReturnsAsync(product);

        var result = await _productService.GetProductByIdAsync(product.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductNotExist()
    {
        _mockRepo.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product?)null);

        var result = await _productService.GetProductByIdAsync(999);

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateProductAsync_ShouldReturnCreatedProduct()
    {
        var expectedProduct = ProductFaker.GenerateFakeProduct();
        _mockRepo.Setup(s => s.CreateAsync(expectedProduct)).ReturnsAsync(expectedProduct);

        var result = await _productService.CreateProductAsync(expectedProduct);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldReturnUpdatedProduct()
    {
        var existingProduct = ProductFaker.GenerateFakeProduct();
        var updatedProduct = new Product
        {
            Id = existingProduct.Id,
            ProductName = "Updated Name",
            Price = "19.99m",
            ImageUrl = "UpdatedImageUrl.jpg",
            Description = "Updated Description"
        };
        _mockRepo.Setup(s => s.UpdateAsync(existingProduct.Id, updatedProduct)).ReturnsAsync(updatedProduct);

        var result = await _productService.UpdateProductAsync(existingProduct.Id, updatedProduct);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedProduct);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldReturnNull_WhenProductNotExist()
    {
        var updatedProduct = ProductFaker.GenerateFakeProduct();
        _mockRepo.Setup(s => s.UpdateAsync(It.IsAny<int>(), updatedProduct)).ReturnsAsync((Product?)null);

        var result = await _productService.UpdateProductAsync(999, updatedProduct);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductDeleted()
    {
        var productId = 1;
        _mockRepo.Setup(s => s.DeleteAsync(productId)).ReturnsAsync(true);

        var result = await _productService.DeleteProductAsync(productId);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldBeFalse_WhenProductNotExist()
    {
        _mockRepo.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await _productService.DeleteProductAsync(999);

        result.Should().BeFalse();
    }
}
