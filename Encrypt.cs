using System;

using System.IO;

using System.Security.Cryptography;

namespace ConsoleApplication

{

    class Program

    {

        static void Main(string[] args)

        {

            string plainText = "This is the message that we want to encrypt.";

            string password = "mypassword";

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            

            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())

            {

                rng.GetBytes(salt);

            }

            

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] key = pbkdf2.GetBytes(32);

            

            using (var aes = new RijndaelManaged())

            {

                aes.Key = key;

                aes.IV = pbkdf2.GetBytes(16);

                // Encrypt the message

                using (var memoryStream = new MemoryStream())

                {

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))

                    {

                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                    }

                    byte[] cipherTextBytes = memoryStream.ToArray();

                    Console.WriteLine("Cipher text: {0}", Convert.ToBase64String(cipherTextBytes));

                }

            }

        }

    }

}

