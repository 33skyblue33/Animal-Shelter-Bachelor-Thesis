using Domain.Ports;
using Microsoft.Extensions.Configuration;
using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ExternalAdapters
{
    internal class ArgonPasswordHasher : IPasswordHasher
    {
        private readonly int _degreeOfParallelism;
        private readonly int _iterations;
        private readonly int _memorySizeKB;

        public ArgonPasswordHasher(IConfiguration configuration)
        {
            string? parallelismRaw = configuration["PasswordHasher:Argon2:DegreeOfParallelism"]
                ?? throw new InvalidOperationException("Argon2 DegreeOfParallelism is not configured.");
            if (!int.TryParse(parallelismRaw, out _degreeOfParallelism))
            {
                throw new InvalidOperationException("Invalid format for Argon2 DegreeOfParallelism.");
            }

            string? iterationsRaw = configuration["PasswordHasher:Argon2:Iterations"]
                ?? throw new InvalidOperationException("Argon2 Iterations is not configured.");
            if (!int.TryParse(iterationsRaw, out _iterations))
            {
                throw new InvalidOperationException("Invalid format for Argon2 Iterations.");
            }

            string? memorySizeRaw = configuration["PasswordHasher:Argon2:MemorySizeKB"]
                ?? throw new InvalidOperationException("Argon2 MemorySizeKB is not configured.");
            if (!int.TryParse(memorySizeRaw, out _memorySizeKB))
            {
                throw new InvalidOperationException("Invalid format for Argon2 MemorySizeKB.");
            }
        }

        public string Hash(string password)
        {
            byte[] salt = new byte[16];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using Argon2id argon2 = new(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = _degreeOfParallelism,
                Iterations = _iterations,
                MemorySize = _memorySizeKB
            };

            byte[] hashBytes = argon2.GetBytes(32);

            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hashBytes);

            return $"$argon2id$v=19$m={_memorySizeKB},t={_iterations},p={_degreeOfParallelism}${saltString}${hashString}";
        }

        public bool Verify(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
            {
                return false;
            }

            string[] parts = hash.Split('$');

            if (parts.Length != 6 || parts[1] != "argon2id")
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[4]);
            byte[] storedHash = Convert.FromBase64String(parts[5]);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using Argon2id argon2 = new(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = _degreeOfParallelism,
                Iterations = _iterations,
                MemorySize = _memorySizeKB
            };

            byte[] computedHash = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }
    }
}