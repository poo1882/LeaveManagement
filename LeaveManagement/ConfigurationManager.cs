using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace LeaveManagement
{
    public class ConfigurationManager 
    {
        IConfigurationBuilder builder;
        IConfigurationRoot config;
        
        public ConfigurationManager()
        {
            builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
               .AddJsonFile("appsettings.json");
            
            config = builder.Build();
        }

        
        

        public ConfigurationManager(string jsonFile)
        {
            builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
               .AddJsonFile(jsonFile);


            config = builder.Build();
        }

        public T Get<T>(string key)
        {
            //if (config[key] is T)
            //{
            //    return (T)config[key];
            //}
            //else
            //{
            try
            {
                return (T)Convert.ChangeType(config[key], typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
            //}

        }

        public string Get(string key)
        {
            return config[key];

        }



        
    }
}
