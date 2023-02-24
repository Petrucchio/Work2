using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work2.Service
{
    internal class ConectService
    {
        public static CrmServiceClient GetService()
        {
            string url = "https://org158912cf.crm.dynamics.com/";
            string clientId = "f8d1b742-37df-4cd1-9deb-2efd717664ba";
            string clientSecret = "flf8Q~RAbjrc3U3oCf4EyUxeAulB5.bfEyAavcrL";
            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url={url};AppId={clientId};ClientSecret={clientSecret};");
            return serviceClient;

        }
    }
}
