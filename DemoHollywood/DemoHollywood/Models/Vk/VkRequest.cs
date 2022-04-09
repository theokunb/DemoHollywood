using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DemoHollywood.Services;

namespace DemoHollywood.Models.Vk
{
    public class VkRequest
    {
        public VkRequest(string title,Dictionary<string,string> param)
        {
            this.title = title;
            this.param = param;
        }

        private string title;
        private Dictionary<string, string> param;


        public string Title => title;
        public Dictionary<string, string> Param => param;


        public bool ChangeParam(string key,string val)
        {
            if (Param.Keys.Contains(key))
            {
                param[key] = val;
                return true;
            }
            else 
                return false;
        }

        public async Task<string> SendRequest(VkClient vkClient)
        {
            return await vkClient.TestMethod1(title, Param);
        }
    }
}
