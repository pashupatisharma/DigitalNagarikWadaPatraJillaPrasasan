using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HelpDesk.Helper
{

    public class EncryptionTechnique
    {
        private string _key = "Banaspati_Document_Tracking";

        public EncryptionTechnique()
        {
        }


        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }
            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                encryptionPrivateKey = this._key;
            }
            TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider()
            {
                Key = (new ASCIIEncoding()).GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = (new ASCIIEncoding()).GetBytes(encryptionPrivateKey.Substring(8, 8))
            };
            byte[] buffer = Convert.FromBase64String(cipherText);


            using (MemoryStream ms = new MemoryStream(buffer))
            {

                using (CryptoStream cs = new CryptoStream(ms, (new TripleDESCryptoServiceProvider()).CreateDecryptor(tDESalg.Key, tDESalg.IV), CryptoStreamMode.Read))
                {
                    try
                    {
                        str = (new StreamReader(cs, new UnicodeEncoding())).ReadLine();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return str;

        }

        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }
            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                encryptionPrivateKey = this._key;
            }
            TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider()
            {
                Key = (new ASCIIEncoding()).GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = (new ASCIIEncoding()).GetBytes(encryptionPrivateKey.Substring(8, 8))
            };

            byte[] array;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, (new TripleDESCryptoServiceProvider()).CreateEncryptor(tDESalg.Key, tDESalg.IV), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = (new UnicodeEncoding()).GetBytes(plainText);
                    cs.Write(toEncrypt, 0, (int)toEncrypt.Length);
                    cs.FlushFinalBlock();
                }
                array = ms.ToArray();
            }
            return Convert.ToBase64String(array);



        }


    }
}