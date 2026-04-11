using MediatR;
using FreshCart.Application.DTOs;
using FreshCart.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FreshCart.Application.Commands.Auth;

public class RegisterUserCommand : IRequest<UserDto>
{
    public RegisterUserDto RegisterDto { get; set; } = null!;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IEmailService emailService,
        IMapper mapper,
        ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerDto = request.RegisterDto;
        
        // Check if email already exists
        var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("An account with this email already exists.");
        }

        // Hash password
        var passwordHash = _passwordHasher.HashPassword(registerDto.Password);

        // Create user entity
        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = registerDto.FullName,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            Mobile = registerDto.Mobile,
            Role = Enum.Parse<UserRole>(registerDto.Role),
            IsVerified = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Save user
        await _userRepository.CreateAsync(user);

        // Send verification email
        await _emailService.SendVerificationEmailAsync(user.Email, user.UserId);

        _logger.LogInformation("User registered successfully: {Email}", user.Email);

        return _mapper.Map<UserDto>(user);
    }
}

// Interfaces (to be implemented in Infrastructure layer)
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
}

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

public interface IEmailService
{
    Task SendVerificationEmailAsync(string email, Guid userId);
    Task SendPasswordResetEmailAsync(string email, string resetToken);
}