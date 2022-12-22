using System;
using System.Security.Cryptography;
using System.Text;

namespace ShareLogic
{
    public class Cryptogram 
    {
        private static RSACryptoServiceProvider _rsaEncryptCrypto;
        private static RSACryptoServiceProvider _rsaDecryptCrypto;

        static RSACryptoServiceProvider _dummy;
        public static void SetPublicKey(string publicKey)
        {
            _rsaEncryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaEncryptCrypto.FromXmlString(publicKey);
        }
        public static void SetPrivateKey(string privateKey)
        {
            _rsaDecryptCrypto = new RSACryptoServiceProvider(2048);
            _rsaDecryptCrypto.FromXmlString(privateKey);
        }
        public static string Encrypt(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Encrypt(bytes);
        }
        public static string Encrypt(byte[] bytes)
        {
            var encryptBytes = _rsaEncryptCrypto.Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }

        public static Tuple<string, string> GetKeyString()
        {
            _dummy = new RSACryptoServiceProvider(2048);
            return Tuple.Create(_dummy.ToXmlString(true), _dummy.ToXmlString(false));
        }
        public static string Decrypt(string value)
        {
            var bytes = Convert.FromBase64String(value);
            return Decrypt(bytes);
        }
        public static string Decrypt(byte[] bytes)
        {
            var decryptBytes = _rsaDecryptCrypto.Decrypt(bytes, false);
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
