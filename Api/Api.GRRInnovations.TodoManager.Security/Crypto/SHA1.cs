using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;
using System;
namespace Api.GRRInnovations.TodoManager.Security.Crypto
{
    public static class SHA1
    {
        public static string Hash(string input)
        {
            var bytes = Strings.ToAsciiByteArray(input);
            var md5 = new Sha1Digest();

            md5.BlockUpdate(bytes, 0, bytes.Length);
            var result = new byte[md5.GetDigestSize()];
            md5.DoFinal(result, 0);

            var hash = BitConverter.ToString(result).ToLower().Replace("-", String.Empty);
            return hash;
        }
    }
}
