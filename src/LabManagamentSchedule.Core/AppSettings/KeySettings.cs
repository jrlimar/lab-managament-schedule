namespace LabManagamentSchedule.Core.AppSettings
{
    public class KeySettings
    {
        /// <summary>     
        /// Base 64 (Chave Interna)         
        /// A chave é "Criptografias com Rijndael / AES"     
        /// </summary> 
        public string CryptoKey { get; set; }

        /// <summary>
        /// 16 bytes
        /// </summary>
        public string BIV { get; set; }
    }
}
