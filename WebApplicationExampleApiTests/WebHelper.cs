using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace WebApplicationExampleApiTests
{
    public class WebHelper
    {
        public String GetResponseByGetQuery(String address, params string[] parameters)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(address);
            }
        }

        public String GetErrorMessageResponseByGetQuery(String address)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    return client.DownloadString(address);
                }
                catch(WebException ex)
                {
                    var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    var jsonObject = Json.Decode(resp);
                    return jsonObject.Message;
                }
            }
        }
    }
}
