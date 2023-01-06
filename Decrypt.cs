using System;

using System.IO;

using System.Security.Cryptography;

namespace ConsoleApplication

{

    class Program

    {

        static void Main(string[] args)

        {

            string cipherText = "KpO+pY+o04v4e4V7z8M1wI2Q+L1vUqW6c8P6oC9YU5k=";

            string password = "mypassword";

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            

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

                // Decrypt the message

                using (var memoryStream = new MemoryStream(cipherTextBytes))

                {

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))

                    {

                        using (var streamReader = new StreamReader(cryptoStream))

                        {

                            string plainText = streamReader.ReadToEnd();

                            Console.WriteLine("Plain text: {0}", plainText);

                        }

                    }

                }

            }

        }

    }

}

