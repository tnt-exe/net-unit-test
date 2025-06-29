using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestProject.Faker;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace TestProject.ControllerTest;
public class ProductControllerTest
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductsController _controller;

    public ProductControllerTest()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductsController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithProducts()
    {
        var productCount = 5;
        var productList = ProductFaker.GenerateFakeProductList(productCount);

        _mockService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(productList);

        var result = await _controller.GetAll();

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(productList);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithProduct_WhenExists()
    {
        var product = ProductFaker.GenerateFakeProduct();
        _mockService.Setup(s => s.GetProductByIdAsync(product.Id)).ReturnsAsync(product);

        var result = await _controller.Get(product.Id);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockService.Setup(s => s.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((Product?)null);

        var result = await _controller.Get(999);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithProduct()
    {
        var product = ProductFaker.GenerateFakeProduct();
        _mockService.Setup(s => s.CreateProductAsync(product)).ReturnsAsync(product);

        var result = await _controller.Create(product);

        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(_controller.Get));
        createdResult.Value.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedProduct_WhenExists()
    {
        var product = ProductFaker.GenerateFakeProduct();
        _mockService.Setup(s => s.UpdateProductAsync(product.Id, product)).ReturnsAsync(product);

        var result = await _controller.Update(product.Id, product);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenProductDoesNotExist()
    {
        var product = ProductFaker.GenerateFakeProduct();
        _mockService.Setup(s => s.UpdateProductAsync(product.Id, product)).ReturnsAsync((Product?)null);

        var result = await _controller.Update(product.Id, product);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenProductDeleted()
    {
        _mockService.Setup(s => s.DeleteProductAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockService.Setup(s => s.DeleteProductAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await _controller.Delete(999);

        result.Should().BeOfType<NotFoundResult>();
    }
}
