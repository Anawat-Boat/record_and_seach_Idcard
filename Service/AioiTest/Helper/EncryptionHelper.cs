using System.Security.Cryptography;
using System.Text;

namespace AioiTest.Helper
{
    public class EncryptionHelper
    {
        private static readonly string Key = AppConfig.GetEncryptionKey();
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText), "Input text cannot be null or empty.");
            }

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetKeyBytes();
                aes.GenerateIV(); 

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); 
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetKeyBytes();
                byte[] iv = new byte[aes.IV.Length];
                Array.Copy(fullCipher, iv, iv.Length); // ดึงค่า IV ออกมา

                using (MemoryStream ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static byte[] GetKeyBytes()
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
            if (keyBytes.Length != 32)
            {
                throw new InvalidOperationException("The encryption key must be 32 bytes.");
            }
            return keyBytes;
        }

      
    }
}
