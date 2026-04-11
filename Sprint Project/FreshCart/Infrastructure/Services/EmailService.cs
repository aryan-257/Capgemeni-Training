using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FreshCart.Application.Commands.Auth;

namespace FreshCart.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendVerificationEmailAsync(string email, Guid userId)
    {
        try
        {
            var verificationToken = GenerateVerificationToken(userId);
            var verificationUrl = $"{_configuration["AppSettings:BaseUrl"]}/verify-email?token={verificationToken}";

            // TODO: Implement actual email sending with SendGrid or SMTP
            _logger.LogInformation("Verification email would be sent to {Email} with URL: {Url}", email, verificationUrl);
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending verification email to {Email}", email);
            throw;
        }
    }

    public async Task SendPasswordResetEmailAsync(string email, string resetToken)
    {
        try
        {
            var resetUrl = $"{_configuration["AppSettings:BaseUrl"]}/reset-password?token={resetToken}";

            // TODO: Implement actual email sending with SendGrid or SMTP
            _logger.LogInformation("Password reset email would be sent to {Email} with URL: {Url}", email, resetUrl);
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending password reset email to {Email}", email);
            throw;
        }
    }

    private string GenerateVerificationToken(Guid userId)
    {
        // In a real implementation, this would be a signed token with expiration
        var tokenData = $"{userId}:{DateTime.UtcNow.AddHours(24):O}";
        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(tokenData);
        return Convert.ToBase64String(tokenBytes);
    }
}