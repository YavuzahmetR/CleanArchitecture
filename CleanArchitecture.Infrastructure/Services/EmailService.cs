using CleanArchitecture.Application.Services;
using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailService(IFluentEmail _fluentEmail)
        {
            this._fluentEmail = _fluentEmail;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = await _fluentEmail.To(toEmail)
                .Subject(subject)
                .Body(body)
                .SendAsync();
            if (!email.Successful)
            {
                throw new Exception("Email could not be sent");
            }
        }
    }
}
