using ConsoleTools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPM0PG_HFT_2022231.Client.Readers;
using OPM0PG_HFT_2022231.Client.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Client
{
    public class ReflectedClient
    {
        private readonly string assemblyName;
        private readonly string domain;
        private string[] args;
        private IRestService restService;
        private ConsoleMenu rootMenu;

        public ReflectedClient(string assemblyName, string domain, IRestService restService, string[] args)
        {
            this.assemblyName = assemblyName;
            this.domain = domain;
            this.restService = restService;
            this.args = args;
            Readers = new ConsoleTypeReaderCollection();
            Writers = new ConsoleTypeWriterCollection();
            JsonConverters = new List<JsonConverter>();
            rootMenu = BuildRootMenu();
        }

        public List<JsonConverter> JsonConverters { get; }
        public ConsoleTypeReaderCollection Readers { get; }
        public ConsoleTypeWriterCollection Writers { get; }

        public void Show()
        {
            rootMenu.Show();
        }

        private ConsoleMenu BuildRootMenu()
        {
            var assembly = Assembly.LoadFrom(assemblyName);
            string controllerPostfix = "Controller";
            var controllers = assembly.GetTypes()
                             .Where(t => t.Name.EndsWith(controllerPostfix)).ToArray();
            ConsoleMenu root = new ConsoleMenu(args, 0);
            foreach (var item in controllers)
            {
                var submenu = BuildSubmenu(item);
                root.Add(item.Name.Replace(controllerPostfix, ""), submenu.Show);
            }
            root.Add("Exit", ConsoleMenu.Close);
            return root;
        }

        private ConsoleMenu BuildSubmenu(Type controllerType)
        {
            ConsoleMenu subMenu = new ConsoleMenu(args, 1);
            var methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<HttpMethodAttribute>() is HttpMethodAttribute attribute)
                {
                    subMenu.Add(method.Name, CreateAction(method, attribute));
                }
            }

            subMenu.Add("Exit", ConsoleMenu.Close);
            return subMenu;
        }

        private Action CreateAction(MethodInfo httpMethod, HttpMethodAttribute attribute)
        {
            if (attribute is HttpGetAttribute)
            {
                return CreateGetAction(httpMethod);
            }
            else if (attribute is HttpPostAttribute)
            {
                return CreatePostAction(httpMethod);
            }
            else if (attribute is HttpPutAttribute)
            {
                return CreatePutAction(httpMethod);
            }
            else if (attribute is HttpDeleteAttribute)
            {
                return CreateDeleteAction(httpMethod);
            }
            else throw new NotSupportedException();
        }

        private Action CreateDeleteAction(MethodInfo httpMethod)
        {
            void Delete()
            {
                ParameterInfo[] parameters = httpMethod.GetParameters();
                string requestUrl = ReadRequestUrlParameters(httpMethod, parameters);
                var response = restService.DeleteAsync(requestUrl).Result;
                WriteResponse(httpMethod, response);
                Console.ReadLine();
            }
            return Delete;
        }

        private Action CreateGetAction(MethodInfo httpMethod)
        {
            void Get()
            {
                ParameterInfo[] parameters = httpMethod.GetParameters();
                string requestUrl = ReadRequestUrlParameters(httpMethod, parameters);
                var response = restService.GetAsync(requestUrl).Result;
                WriteResponse(httpMethod, response);
                Console.ReadLine();
            }

            return Get;
        }

        private Action CreatePostAction(MethodInfo httpMethod)
        {
            void Post()
            {
                ParameterInfo[] parameters = httpMethod.GetParameters();
                string requestUrl = GetRequestUrl(httpMethod);
                object content = ReadFromBodyParameter(parameters[0]);
                var response = restService.PostAsync(requestUrl, content, JsonConverters.ToArray()).Result;
                WriteResponse(httpMethod, response);
                Console.ReadLine();
            }
            return Post;
        }

        private Action CreatePutAction(MethodInfo httpMethod)
        {
            void Put()
            {
                ParameterInfo[] parameters = httpMethod.GetParameters();
                string requestUrl = GetRequestUrl(httpMethod);
                object content = ReadFromBodyParameter(parameters[0]);
                var response = restService.PutAsync(requestUrl, content, JsonConverters.ToArray()).Result;
                WriteResponse(httpMethod, response);
                Console.ReadLine();
            };
            return Put;
        }

        private string GetRequestUrl(MethodInfo method)
        {
            return domain + method.DeclaringType.Name.Replace("Controller", "") + "/" + method.Name + "/";
        }

        private object ReadFromBodyParameter(ParameterInfo parameter)
        {
            if (Readers.Contains(parameter.ParameterType))
            {
                return Readers[parameter.ParameterType].Read();
            }
            else throw new NotSupportedException();
        }

        private string ReadRequestUrlParameters(MethodInfo httpMethod, ParameterInfo[] parameters)
        {
            string url = GetRequestUrl(httpMethod);
            List<string> inputs = new List<string>();

            foreach (var item in parameters)
            {
                if (item.GetCustomAttribute<FromBodyAttribute>() is null)
                {
                    Console.Write($"{item.Name}({item.ParameterType.Name}): ");
                    inputs.Add(Console.ReadLine());
                }
            }

            return url + string.Join(",", inputs);
        }

        private void WriteResponse(MethodInfo httpMethod, HttpResponseMessage response)
        {
            Console.WriteLine($"statuscode: {response.StatusCode}");
            string jsonString = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK &&
               httpMethod.ReturnType != typeof(void) &&
               Writers.Contains(httpMethod.ReturnType))
            {
                Writers[httpMethod.ReturnType]
                    .Write(JsonConvert.DeserializeObject(jsonString, httpMethod.ReturnType, JsonConverters.ToArray()));
            }
            else if (!string.IsNullOrWhiteSpace(jsonString))
            {
                try
                {
                    Console.WriteLine(JToken.Parse(jsonString).ToString(Formatting.Indented));
                }
                catch
                {
                    Console.WriteLine(jsonString);
                }
            }
        }
    }
}