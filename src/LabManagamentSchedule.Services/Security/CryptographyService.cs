using LabManagamentSchedule.Core.AppSettings;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LabManagamentSchedule.Services.Security
{
    public class CryptographyService : ICryptographyService
    {
        private readonly KeySettings keyCryptography;
        public CryptographyService(KeySettings keyCryptography)
        {
            this.keyCryptography = keyCryptography;
        }

        public string Decrypt(string text)
        {
            try
            {
                byte[] bKey = Convert.FromBase64String(keyCryptography.CryptoKey);
                byte[] bText = Convert.FromBase64String(text);
                byte[] bIV = new UTF8Encoding().GetBytes(keyCryptography.BIV);

                Rijndael rijndael = new RijndaelManaged();
                rijndael.KeySize = 256;

                MemoryStream mStream = new MemoryStream();

                CryptoStream decryptor = new CryptoStream(mStream, rijndael.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);
                decryptor.Write(bText, 0, bText.Length);
                decryptor.FlushFinalBlock();

                UTF8Encoding utf8 = new UTF8Encoding();

                return utf8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao descriptografar", ex);
            }
        }
    }
}
