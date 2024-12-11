﻿using System.Security.Cryptography;
using System.Text;

public static class HashingHelper
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        string hashedPassword = HashPassword(password);
        return hashedPassword == storedHash;
    }
}
