using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Test.Tests.Common.Services;

public class InMemoryEmailService : IEmailService
{
    public ConcurrentBag<(string To, string Subject, string Body)> SentEmails { get; } = new();

    public Task SendEmailAsync(string to, string subject, string body)
    {
        SentEmails.Add((to, subject, body));
        return Task.CompletedTask;
    }
}
