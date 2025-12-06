using Domain.Models;

namespace Test.Tests.Data.User;

public static class UserData
{
    public static Domain.Models.User FirstUser()
    {
        return Domain.Models.User.Register(
            Guid.NewGuid(),
            "John",
            "Doe",
            "john.doe@example.com"
        );
    }

    public static Domain.Models.User SecondUser()
    {
        return Domain.Models.User.Register(
            Guid.NewGuid(),
            "Jane",
            "Smith",
            "jane.smith@example.com"
        );
    }
}
