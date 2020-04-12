using System.Security.Cryptography;
using System.Text;

namespace SystemUnderTest
{
    public class Md5Helper
    {
        public string Hash(string input)
        {
            var inputInBytes = Encoding.UTF8.GetBytes(input);
            var hashInBytes = MD5.Create().ComputeHash(inputInBytes);

            var stringBuilder = new StringBuilder();
            foreach (var @byte in hashInBytes)
            {
                stringBuilder.Append(@byte.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}