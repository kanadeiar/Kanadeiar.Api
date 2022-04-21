using FluentAssertions;
using Kanadeiar.Api.Registrations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
namespace Kanadeiar.Api.Tests.Registrations;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void KarAddSwagger_Correct_ShouldType()
    {
        var services = Mock.Of<IServiceCollection>();

        var actual = ServiceCollectionExtensions.KndAddSwagger(services, "testTitle");

        actual.Should().NotBeNull();
    }

    [Fact]
    public void KarAddMapster_Correct_ShoultDype()
    {
        var services = Mock.Of<IServiceCollection>();

        var actual = ServiceCollectionExtensions.KndAddMapster(services);

        actual.Should().NotBeNull();
    }
}
