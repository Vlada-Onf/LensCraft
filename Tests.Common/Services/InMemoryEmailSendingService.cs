using System.Collections.Concurrent;
using Application.Common.Interfaces;

namespace Test.Tests.Common.Services
{
    public class InMemoryEmailSendingService : IEmailSendingService
    {
        public ConcurrentBag<(string To, string Body)> SentEmails { get; } = new();

        public Task SendEmailAsync(string emailTo, string emailBody)
        {
            SentEmails.Add((emailTo, emailBody));
            return Task.CompletedTask;
        }
    }
}
