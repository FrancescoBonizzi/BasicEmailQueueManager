﻿using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground
{
    public class FakeEmailClient : IEmailClient
    {
        public async Task Send(Email email)
        {
            await Task.Delay(200);
            Console.WriteLine($"SENT! From: {email.From.Trim()}, To: {email.To.Trim()}, Body: {email.Body.Trim()}");
        }
    }
}
