using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using LeaveManagement.Models;

namespace LeaveManagement.Apis
{
    public class GoogleServices
    {

        private ConfigurationManager _config = new ConfigurationManager();
        private string _getUserEndpoint;

        public GoogleServices()
        {
            _getUserEndpoint = _config.Get<string>("https://www.googleapis.com/admin/directory/v1/users/");
        }


       public async Task<UserInfoModel> GetUserInfoByEmailAsync(string email)
        {
var result = await _getUserEndpoint.GetJsonAsync<UserInfoModel>();
            

            return result;
        }
    }
}
