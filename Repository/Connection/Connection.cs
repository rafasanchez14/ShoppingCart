using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Connection
{
    public class BaseConnection
    {
        private IConfiguration _config;

        public BaseConnection(IConfiguration config)
        {
            _config = config;
        }

        public  string GetConectionString() 
        {
            return _config.GetSection("MySettings").GetSection("DbConnection").Value;
          
        }
 

    }
}