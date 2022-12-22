using System.Security.Cryptography;
using System.Text;

namespace ShareLogic
{
    public class Cryptogram 
    {
        private static RSACryptoServiceProvider _rsaEncryptCrypto = new RSACryptoServiceProvider();
        private static RSACryptoServiceProvider _rsaDecryptCrypto = new RSACryptoServiceProvider();
        public static void SetPublicKey(string publicKey)
        {
            _rsaEncryptCrypto.FromXmlString(publicKey);
        }
        public static void SetPrivateKey(string privateKey)
        {
            _rsaDecryptCrypto.FromXmlString(privateKey);
        }
        public static string Encrypt(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var encryptBytes = _rsaEncryptCrypto.Encrypt(bytes, false);
            return Encoding.UTF8.GetString(encryptBytes);
        }
        public static string GetPrivateKey()
        {
            var privateKey = RSA.Create().ExportParameters(true);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ImportParameters(privateKey);
            return provider.ToXmlString(true);
        }

        public static string GetPublicKey()
        {
            return _rsaEncryptCrypto.ToXmlString(false);
        }
        public static string Decrypt(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var decryptBytes = _rsaDecryptCrypto.Decrypt(bytes, false);
            return Encoding.UTF8.GetString(decryptBytes);
        }
    }
}
