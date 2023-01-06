# Text-encryption-and-decryption
this code is able to encrypt text in csharp 
This code first converts the plain text message to a byte array, generates a random salt, and derives a key from the password and salt using PBKDF2 (Password-Based Key Derivation Function 2). It then creates a Rijndael object and sets its key and initialization vector (IV) to the derived key and a portion of the derived bytes, respectively. Finally, it uses a CryptoStream object to perform the encryption, and converts the resulting ciphertext to a base64 string for printing.
