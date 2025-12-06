using Domain.Models;

namespace Tests.Data.User;

public static class UserData
{
    public static User FirstUser() =>
        User.Create(
            Guid.NewGuid(),
            "Vlada",
            "Lens",
            "vlada@lenscraft.com",
            DateTime.UtcNow
        );

    public static User SecondUser() =>
        User.Create(
            Guid.NewGuid(),
            "Alex",
            "Viewer",
            "alex@lenscraft.com",
            DateTime.UtcNow
        );
}
