using FluentAssertions;
using Kanadeiar.Api.Filters;
using Xunit;

namespace Kanadeiar.Api.Tests.Filters;

public class ExceptionHandlingAttributeTests
{
    [Fact]
    public void InitAttrubute_Init_CorrectType()
    {
        var attribute = new ExceptionHandlingAttribute();
                
        attribute.Should().BeOfType<ExceptionHandlingAttribute>();
    }
}
