using System.Configuration;

namespace ChatRoom
{
    public class ApplicationConnection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}