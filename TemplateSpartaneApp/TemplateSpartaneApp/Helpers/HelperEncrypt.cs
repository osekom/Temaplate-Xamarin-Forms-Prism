using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TemplateSpartaneApp.Helpers
{
    public static class HelperEncrypt
    {
        public static string EncryptPassword(string pass)
        {
            if (!string.IsNullOrEmpty(pass))
            {
                return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(pass)).Select(s => s.ToString("x2")));
            }
            return null;
        }
    }
}
