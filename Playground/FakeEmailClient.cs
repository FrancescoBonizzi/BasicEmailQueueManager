using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using System;

namespace Playground
{
    public class FakeEmailClient : IEmailClient
    {
        public void Send(Email email)
        {
            Console.WriteLine($"SENT! From: {email.From}, To: {email.To}, BodY: {email.Body}");
        }
    }
}
