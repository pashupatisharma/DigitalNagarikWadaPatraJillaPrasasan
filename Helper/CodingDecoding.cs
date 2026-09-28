using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HelpDesk.Helper
{
    public static class CodingDecoding
    {
        public static string DecodeFromDecode64(this string encodedData)
        {
            if (string.IsNullOrEmpty(encodedData))
            {
                return null;
            }
            Decoder decoder = (new UTF8Encoding()).GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            char[] decoded_char = new char[decoder.GetCharCount(todecode_byte, 0, (int)todecode_byte.Length)];
            decoder.GetChars(todecode_byte, 0, (int)todecode_byte.Length, decoded_char, 0);
            return new string(decoded_char);
        }

        public static string EncodeToEncodeBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return null;
            }
            byte[] numArray = new byte[plainText.Length];
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }
    }
}