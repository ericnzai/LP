using System;
using System.Security.Cryptography;
using System.Text;
using LP.Api.Shared.Interfaces.Core.Encryption;

namespace LP.Core.Encryption
{
    public class EncryptionHandler : IEncryptionHandler
    {
        private readonly byte[] _symetricAlgorithmInitializationVector = { 32, 25, 216, 48, 27, 64, 33, 16 };

        /// <summary>
        ///     The second part of the encryption key used by TripleDES
        /// </summary>
        private const string SecretKey = "25243a#FN66F£3s1ldfbvcm9";

        private readonly ICryptoTransform _cryptoTransformEncryptor;
        private readonly ICryptoTransform _cryptoTransformDecryptor;

        public EncryptionHandler()
        {
            var tripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(SecretKey), IV = _symetricAlgorithmInitializationVector };
            _cryptoTransformEncryptor = tripleDesCryptoServiceProvider.CreateEncryptor();
            _cryptoTransformDecryptor = tripleDesCryptoServiceProvider.CreateDecryptor();
        }

        /// <summary>
        ///     The methods takes a plain string and enrypts it using Triple DES
        /// </summary>
        /// <param name="text">The string to encrypt</param>
        /// <example>
        ///     <code>
        /// string encryptedString = EncryptString("testString");
        /// </code>
        /// </example>
        /// <returns>Encrypted string</returns>
        public string EncryptString(string text)
        {
            try
            {
                if (text.Length >= 2 && text.Substring(0, 2) != "~~")
                {
                    var buffer = Encoding.UTF8.GetBytes(text);
                    return "~~" + Convert.ToBase64String(_cryptoTransformEncryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }

                if (text.Length == 1)
                {
                    var buffer = Encoding.ASCII.GetBytes(text);
                    return "~~" + Convert.ToBase64String(_cryptoTransformEncryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }

                return text;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception encrypting message: " + ex.Message);
            }
        }

        /// <summary>
        ///     The methods takes an encrypted string and decrypts it using Triple DES
        /// </summary>
        /// <param name="text">The string to decrypt</param>
        /// <example>
        ///     <code>
        /// string decryptedString = DecryptString("!232££776HJ");
        /// </code>
        /// </example>
        /// <returns>Unencrypted string</returns>
        public string DecryptString(string text)
        {
            try
            {
                if (text != null && text.Length >= 2 && text.Substring(0, 2) == "~~")
                {
                    text = text.Replace(' ', '+');
                    text = text.Substring(2);
                    var buffer = Convert.FromBase64String(text);
                    return Encoding.UTF8.GetString(_cryptoTransformDecryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }

                return text;
            }

            catch (Exception ex)
            {
                throw new Exception("Exception decrypting message: " + ex.Message);
            }
        }
    }
}
