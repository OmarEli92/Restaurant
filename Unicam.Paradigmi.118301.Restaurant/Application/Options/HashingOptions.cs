using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace UApplication.Options
{
    public class HashingOptions
{
        public int KeySize { get; set; } = 64;
        public int Iterations { get; set; } = 350000;
        public HashAlgorithmName HashAlgorithm { get; set; } = HashAlgorithmName.SHA256;
    }
}
