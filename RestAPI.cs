using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Saby7
{
    class RestAPI
    {

        public static T GetTwithoutParams<T>(string controllerName, string method, string APIServerUrl = "APIServerUrl", string APIServerUrlPort = "APIServerUrlPort") where T : new()
        {
            T n = new T();

            var client = new RestClient("https://" + ConfigurationManager.AppSettings[APIServerUrl] + ":" + ConfigurationManager.AppSettings[APIServerUrlPort] + "/api/" + controllerName + "/" + method);
            var request = new RestRequest(RestSharp.Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var rsp = response.Content;
                n = JsonConvert.DeserializeObject<T>(rsp);
            }

            return n;
        }

        public static T GetTwithParams<T>(string controllerName, string method, string param, string APIServerUrl = "APIServerUrl", string APIServerUrlPort = "APIServerUrlPort") where T : new()
        {
            T n = new T();
            var client = new RestClient("https://" + ConfigurationManager.AppSettings[APIServerUrl] + ":" + ConfigurationManager.AppSettings[APIServerUrlPort] + "/api/" + controllerName + "/" + method + "?" + param);
            var request = new RestRequest(RestSharp.Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var rsp = response.Content;
                n = JsonConvert.DeserializeObject<T>(rsp);
            }

            return n;
        }
        public static T PostMethod<T>(string controllerName, string methodUrl, object obj, string APIServerUrl = "APIServerUrl", string APIServerUrlPort = "APIServerUrlPort") where T : new()
        {
            T n = new T();
            var client = new RestClient("https://" + ConfigurationManager.AppSettings[APIServerUrl] + ":" + ConfigurationManager.AppSettings[APIServerUrlPort] + "/api/" + controllerName + "/" + methodUrl);
            var request = new RestRequest(Method.POST);
            string objs = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            request.AddParameter("undefined", objs, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var rsp = response.Content;
                if (rsp != "bad client request")
                {
                    try
                    {
                        n = JsonConvert.DeserializeObject<T>(rsp);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return n;
        }

    }
}
