using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Util;

namespace Domain.Services
{
    public class AuthService(IUserRepository userRepository, IVerificationTokenRepository verificationTokenRepository, IEmailService emailService, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork) : IAuthService
    {
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            User? user = await userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new InvalidEmailException($"User with email {email} does not exist.");
            }

            if (!passwordHasher.Verify(password, user.PasswordHash))
            {
                throw new InvalidPasswordException($"Invalid password.");
            }

            return await Authenticate(user);
        }

        public async Task LogoutAsync(string token)
        {
            RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken == null)
            {
                throw new ExpiredRefreshTokenException($"Session expired.");
            }

            refreshToken.Revoked = DateTime.UtcNow;
            await unitOfWork.CompleteAsync();
        }

        public async Task<AuthResult> RefreshAsync(string token)
        {
            RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken == null)
            {
                throw new ExpiredRefreshTokenException($"Session expired.");
            }

            User? user = await userRepository.GetByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                throw new ExpiredRefreshTokenException($"Session expired.");
            }

            refreshToken.Revoked = DateTime.UtcNow;
            return await Authenticate(user);
        }

        public async Task RegisterAsync(UserRegisterCommand command)
        {
            if (await userRepository.GetByEmailAsync(command.Email) != null)
            {
                throw new UserAlreadyExistsException($"User with email {command.Email} already exists.");
            }

            User user = new()
            {
                Email = command.Email,
                Name = command.Name,
                Age = command.Age,
                PasswordHash = passwordHasher.Hash(command.Password),
                Role = UserRole.Unverified
            };

            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            VerificationToken token = tokenGenerator.GenerateVerificationToken(user);

            await verificationTokenRepository.AddAsync(token);
            await emailService.SendVerificationEmailAsync(user, token);

            await unitOfWork.CompleteAsync();
        }

        public async Task VerifyAsync(string token)
        {
            VerificationToken? verificationToken = await verificationTokenRepository.GetByTokenAsync(token);

            if (verificationToken == null)
            {
                throw new VerificationException($"Unexpected error during verification. Verification token doesn't exist.");
            }

            User? user = await userRepository.GetByIdAsync(verificationToken.UserId);

            if (user == null)
            {
                throw new VerificationException($"Unexpected error during verification. User doesn't exist.");
            }

            if (verificationToken.Expires < DateTime.UtcNow)
            {
                VerificationToken newToken = tokenGenerator.GenerateVerificationToken(user);
                await verificationTokenRepository.AddAsync(newToken);
                await emailService.SendVerificationEmailAsync(user, newToken);
                await unitOfWork.CompleteAsync();
                throw new VerificationTokenExpiredException($"Verification link expired. An email with new link has been sent.");
            }

            user.Role = UserRole.User;
            verificationToken.Revoked = DateTime.UtcNow;
            await unitOfWork.CompleteAsync();
        }

        private async Task<AuthResult> Authenticate(User user)
        {
            RefreshToken refreshToken = tokenGenerator.GenerateRefreshToken(user);
            string accessToken = tokenGenerator.GenerateAccessToken(user);

            await refreshTokenRepository.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            return new AuthResult(accessToken, refreshToken, user);
        }
    }
}
