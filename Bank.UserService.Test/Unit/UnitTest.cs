using Shouldly;

using Xunit;

namespace Bank.UserService.Test.Unit;

public class UnitTest
{
    [Fact]
    public void Add_TwoNumbers_ReturnsSum()
    {
        int a = 1;
        int b = 2;
        // what the fuck is this
        (a + b).ShouldBe(3);
    }
}
