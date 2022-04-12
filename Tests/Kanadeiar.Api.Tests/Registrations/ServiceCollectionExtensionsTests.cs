using FluentAssertions;
using Kanadeiar.Api.Registrations;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
namespace Kanadeiar.Api.Tests.Registrations;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddServiceSwagger_Correct_ShouldType()
    {
        var services = Mock.Of<IServiceCollection>();

        var actual = ServiceCollectionExtensions.KanadeiarAddSwagger(services, "testTitle");

        actual.Should().NotBeNull();
    }
}
