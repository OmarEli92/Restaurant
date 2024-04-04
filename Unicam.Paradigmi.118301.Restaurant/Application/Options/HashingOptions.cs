using System.Security.Cryptography;

namespace UApplication.Options
{
    public class HashingOptions
{
        public int KeySize {  get; set; }
        public int Iterations {  get; set; }
        public HashAlgorithmName HashAlgorithm { get; set; } = HashAlgorithmName.SHA256;
    }
}
