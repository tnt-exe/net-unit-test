using FluentAssertions;
using WebApi;
using WebApi.Controllers;

namespace TestProject.ControllerTest;
public class WeatherForecastControllerTest
{
    [Fact]
    public void Get_ReturnsWeatherForecast()
    {
        var controller = new WeatherForecastController();

        var result = controller.Get();

        result.Should().NotBeNull();
        result.Should().BeOfType<WeatherForecast[]>();
        result.Should().HaveCount(5);
    }
}
