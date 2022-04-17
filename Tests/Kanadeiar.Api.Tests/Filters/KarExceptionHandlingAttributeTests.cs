using FluentAssertions;
using Kanadeiar.Api.Filters;
using Xunit;

namespace Kanadeiar.Api.Tests.Filters;

public class KarExceptionHandlingAttributeTests
{
    [Fact]
    public void InitAttrubute_Init_CorrectType()
    {
        var attribute = new KarExceptionHandlingAttribute();
                
        attribute.Should().BeOfType<KarExceptionHandlingAttribute>();
    }
}
