using MediatR;
using FreshCart.Application.DTOs;
using FreshCart.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace FreshCart.Application.Commands.Auth;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public LoginDto LoginDto { get; set; } = null!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IMapper mapper,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = request.LoginDto;
        
        // Get user by email
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Check if user is verified
        if (!user.IsVerified)
        {
            throw new UnauthorizedAccessException("Please verify your email address before logging in.");
        }

        // Check if user is banned
        if (user.IsBanned)
        {
            throw new UnauthorizedAccessException("Your account has been suspended. Please contact support.");
        }

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(user);
        var expiresAt = DateTime.UtcNow.AddMinutes(15); // 15-minute expiry as per SRS

        _logger.LogInformation("User logged in successfully: {Email}", user.Email);

        return new LoginResponseDto
        {
            AccessToken = token,
            User = _mapper.Map<UserDto>(user),
            ExpiresAt = expiresAt
        };
    }
}

public interface IJwtTokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
    Guid GetUserIdFromToken(string token);
}