using dotenv.net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace dotnet_dotenv_vault
{
    internal static class Reader
    {
        internal static ReadOnlySpan<string> Read(string envFilePath, bool ignoreExceptions, Encoding encoding)
        {
            var defaultResponse = ReadOnlySpan<string>.Empty;

            // if configured to throw errors then throw otherwise return
            if (string.IsNullOrWhiteSpace(envFilePath))
            {
                if (ignoreExceptions)
                {
                    return defaultResponse;
                }

                throw new ArgumentException("The file path cannot be null, empty or whitespace.", nameof(envFilePath));
            }

            // if configured to throw errors then throw otherwise return
            if (!File.Exists(envFilePath))
            {
                if (ignoreExceptions)
                {
                    return defaultResponse;
                }

                throw new FileNotFoundException($"A file with provided path \"{envFilePath}\" does not exist.");
            }

            // read all lines from the env file
            var rawAllLinesNew = ParseVault();
            return rawAllLinesNew;
        }

        private static string[] Decrypt(string[] rawAllLines)
        {
            foreach (var line in rawAllLines)
            {
                Debug.WriteLine(line);
            }
            throw new NotImplementedException();
        }

        public static ReadOnlySpan<string> ParseVault()
        {
            var dotenvKey = Environment.GetEnvironmentVariable("DOTENV_KEY");
            if (dotenvKey == null)
                throw new KeyNotFoundException("NOT_FOUND_DOTENV_KEY: Cannot find ENV['DOTENV_KEY']");

            // Parse DOTENV_KEY. Format is a URI
            // dotenv://:key_1234@dotenv.org/vault/.env.vault?environment=production
            var uri = new Uri(dotenvKey);

            // Get decrypt key
            var uriUserInfoParts = uri.UserInfo.Split(':', StringSplitOptions.None);
            var key = uriUserInfoParts[1];
            if (key == null)
                throw new InvalidDataException("INVALID_DOTENV_KEY: Missing key part");

            // Get environment
            var param = HttpUtility.ParseQueryString(uri.Query);
            var environment = param.Get("environment");
            if (key == null)
                throw new InvalidDataException("INVALID_DOTENV_KEY: Missing environment part");

            // Parse .env.vault
            var parsed = DotEnv.Read(new DotEnvOptions(true, new string[] { ".env.vault" }));

            // Get ciphertext
            var environmentKey = $"DOTENV_VAULT_{environment.ToUpper()}";
            var ciphertext = parsed[environmentKey]; // DOTENV_VAULT_PRODUCTION
            if (ciphertext == null)
                throw new Exception("NOT_FOUND_DOTENV_ENVIRONMENT: Cannot locate #{environment_key} in .env.vault");

            // Decrypt ciphertext
            var decrypted = Decrypt(ciphertext, key);
            return decrypted;
        }

        private static ReadOnlySpan<string> Decrypt(string cipherText, string key)
        {
            // Decode
            Span<byte> encryptedData = Convert.FromBase64String(cipherText).AsSpan();
            var trimmedKey = StringToByteArray(key[^64..]);

            // Extract parameter sizes
            int cipherSize = encryptedData.Length - 12 - 16;

            // Extract parameters
            var nonce = encryptedData.Slice(0, 12);
            var tag = encryptedData.Slice(encryptedData.Length - 16);
            var cipherBytes = encryptedData.Slice(12, cipherSize);

            // Decrypt
            Span<byte> plainBytes = cipherSize < 1024
                                  ? stackalloc byte[cipherSize]
                                  : new byte[cipherSize];
            using var aes = new AesGcm(trimmedKey);
            aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

            // Convert plain bytes back into string
            var plaintext = Encoding.ASCII.GetString(plainBytes);

            var splits = plaintext.Split("\n");
            return new ReadOnlySpan<string>(splits);
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}