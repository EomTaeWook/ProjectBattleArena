using Kosher.Framework;
using System.Text;

namespace GameWebServer.Manager
{
    public class AuthManager : Singleton<AuthManager>
    {
        private static char[] CharCandidates = new char[] {
            '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm',
            'n', 'o', 'p','q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
            'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        };
        private const int PasswordMaxSize = 50;
        public string GetPassword()
        {
            var random = new Random(DateTime.Now.Ticks.GetHashCode());

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < PasswordMaxSize; ++i)
            {
                int index = random.Next(CharCandidates.Length);
                sb.Append(CharCandidates.ElementAt(index));
            }

            return sb.ToString();
        }
    }
}
