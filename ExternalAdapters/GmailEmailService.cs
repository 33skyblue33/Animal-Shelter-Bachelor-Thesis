using Domain.Entities;
using Domain.Ports;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ExternalAdapters
{
    internal class GmailEmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromAddress;
        private readonly string _fromName;
        private readonly string _verificationBaseUrl;
        private readonly string _verificationSubjectTemplate;
        private readonly string _verificationBodyTemplate;

        public GmailEmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["Email:Smtp:Host"]
                ?? throw new InvalidOperationException("Email:Smtp:Host is not configured.");

            string? portRaw = configuration["Email:Smtp:Port"]
                ?? throw new InvalidOperationException("Email:Smtp:Port is not configured.");
            if (!int.TryParse(portRaw, out _smtpPort))
            {
                throw new InvalidOperationException("Invalid format for Email:Smtp:Port.");
            }

            _smtpUsername = configuration["Email:Smtp:Username"]
                ?? throw new InvalidOperationException("Email:Smtp:Username is not configured.");

            _smtpPassword = configuration["Email:Smtp:Password"]
                ?? throw new InvalidOperationException("Email:Smtp:Password is not configured.");

            _fromAddress = configuration["Email:From:Address"]
                ?? throw new InvalidOperationException("Email:From:Address is not configured.");

            _fromName = configuration["Email:From:Name"]
                ?? throw new InvalidOperationException("Email:From:Name is not configured.");

            _verificationBaseUrl = configuration["Email:Verification:BaseUrl"]
                ?? throw new InvalidOperationException("Email:Verification:BaseUrl is not configured.");

            _verificationSubjectTemplate = configuration["Email:Verification:Subject"]
                ?? throw new InvalidOperationException("Email:Verification:Subject is not configured.");

            _verificationBodyTemplate = configuration["Email:Verification:BodyTemplate"]
                ?? throw new InvalidOperationException("Email:Verification:BodyTemplate is not configured.");
        }

        public async Task SendVerificationEmailAsync(User user, VerificationToken token)
        {
            string verificationUrl = $"{_verificationBaseUrl}?token={token.Token}";

            string subject = _verificationSubjectTemplate
                .Replace("{AppName}", _fromName);

            string body = _verificationBodyTemplate
                .Replace("{UserName}", user.Name)
                .Replace("{VerificationUrl}", verificationUrl)
                .Replace("{ExpiresUtc:F}", token.Expires.ToString("F"));

            using SmtpClient smtpClient = new(_smtpHost)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true,
            };

            MailMessage mailMessage = new()
            {
                From = new MailAddress(_fromAddress, _fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(user.Email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}