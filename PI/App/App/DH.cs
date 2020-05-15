using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class DH : IDisposable
    {
        private Aes aes = null;
        private ECDiffieHellmanCng df = null;
        private readonly byte[] publicKey;

        public DH()
        {
            aes = new AesCryptoServiceProvider();
            df = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            };
            publicKey = df.PublicKey.ToByteArray();
        }

        public byte[] PublicKey { get { return publicKey; } }
        public byte[] IV { get { return aes.IV; } }

        public byte[] Encrypt(byte[] publicKey, byte[] secretMessage)
        {
            byte[] encryptedMessage;
            var key = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
            var derivedKey = this.df.DeriveKeyMaterial(key);
            this.aes.Key = derivedKey;
            using (var cipherText = new MemoryStream())
            {
                using (var encryptor = this.aes.CreateEncryptor())
                {
                    using (var cryptoStream = new CryptoStream(cipherText, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] ciphertextMessage = secretMessage;
                        cryptoStream.Write(ciphertextMessage, 0, ciphertextMessage.Length);
                    }
                }
                encryptedMessage = cipherText.ToArray();
            }
            return encryptedMessage;
        }

        public byte[] Decrypt(byte[] publicKey, byte[] encryptedMessage, byte[] iv)
        {
            byte[] decryptedMessage;
            var key = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
            var derivedKey = this.df.DeriveKeyMaterial(key);
            this.aes.Key = derivedKey;
            this.aes.IV = iv;
            using (var plainText = new MemoryStream())
            {
                using (var decryptor = this.aes.CreateDecryptor())
                {
                    using (var cryptoStream = new CryptoStream(plainText, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedMessage, 0, encryptedMessage.Length);
                    }
                }
                decryptedMessage = plainText.ToArray();
            }
            return decryptedMessage;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (aes != null)
                    aes.Dispose();
                if (df != null)
                    df.Dispose();
            }
        }
    }

}
