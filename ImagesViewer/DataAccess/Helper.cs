using System.Configuration;

namespace DataAccess
{
    public static class Helper
    {
        public static string CnnVal(string cnnName)
        {
            return ConfigurationManager.ConnectionStrings[cnnName].ConnectionString;
        }
    }
}
