﻿using ConsoleTools;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPM0PG_HFT_2022231.Client.Readers;
using OPM0PG_HFT_2022231.Client.Writers;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using OPM0PG_HFT_2022231.Models.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Client
{
    public class MapperClient
    {
        private readonly string apiInterfaceMapUri;
        private string[] args;
        private IRestService restService;
        private ConsoleMenu rootMenu;
        private Dictionary<string, Type> types;

        public MapperClient(string apiInterfaceMapUri,
                               IRestService restService,
                               string[] args)
        {
            this.apiInterfaceMapUri = apiInterfaceMapUri;
            this.restService = restService;
            this.args = args;
            Readers = new ConsoleTypeReaderCollection();
            Writers = new ConsoleTypeWriterCollection();
            types = new Dictionary<string, Type>();
            AddDefaultReaders();
        }

        public ConsoleTypeReaderCollection Readers { get; }
        public ConsoleTypeWriterCollection Writers { get; }

        public string ReadRequestUriParameters(ApiMethodDTO method)
        {
            string url = method.RequestUri;
            List<string> inputs = new List<string>();
            foreach (var item in method.Parameters)
            {
                Type param = GetType(method.Parameters.First().AssemblyQTypeName);

                Console.Write($"{item.Name}({param.Name}): ");
                inputs.Add(Console.ReadLine());
            }

            return url + string.Join(",", inputs);
        }

        public void Run()
        {
            rootMenu = BuildRootMenu(GetApiInterfaceMap());
            rootMenu.Show();
        }

        private void AddDefaultReaders()
        {
            var factories = GenericFactory
                                .CreateFactoriesFromEntities<Func<ConsoleTypeReader>>(typeof(EntityReader<>));
            foreach (var factory in factories)
            {
                Readers.Add(factory.Value());
            }
        }

        private ConsoleMenu BuildRootMenu(ApiInterfaceMapDTO map)
        {
            ConsoleMenu root = new ConsoleMenu(args, 0);
            foreach (var item in map.Controllers)
            {
                var submenu = BuildSubmenu(item);
                root.Add(item.Name, submenu.Show);
            }
            root.Add("Exit", ConsoleMenu.Close);
            return root;
        }

        private ConsoleMenu BuildSubmenu(ApiControllerDTO controller)
        {
            ConsoleMenu subMenu = new ConsoleMenu(args, 1);
            foreach (var method in controller.Methods)
            {
                subMenu.Add(method.Name, CreateAction(method));
            }

            subMenu.Add("Exit", ConsoleMenu.Close);
            return subMenu;
        }

        private Action CreateAction(ApiMethodDTO method)
        {
            switch (method.MethodType)
            {
                case HttpMethodType.GET: return CreateGetAction(method);
                case HttpMethodType.DELETE: return CreateDeleteAction(method);
                case HttpMethodType.POST: return CreatePostAction(method);
                case HttpMethodType.PUT: return CreatePutAction(method);
                default: throw new NotSupportedException();
            }
        }

        private Action CreateDeleteAction(ApiMethodDTO method)
        {
            void Delete()
            {
                string requestUrl = ReadRequestUriParameters(method);
                var response = restService.DeleteAsync(requestUrl).Result;
                WriteResponse(method, response);
                Console.ReadLine();
            }
            return Delete;
        }

        private Action CreateGetAction(ApiMethodDTO method)
        {
            void Get()
            {
                string paramRequestUri = ReadRequestUriParameters(method);
                var response = restService.GetAsync(paramRequestUri).Result;
                WriteResponse(method, response);
                Console.ReadLine();
            }

            return Get;
        }

        private Action CreatePostAction(ApiMethodDTO method)
        {
            void Post()
            {
                object content = ReadFromBodyParameter(method.Parameters.First()
                                                     ,(p)=>p.GetCustomAttribute<DatabaseGeneratedAttribute>() is not null);
                var response = restService.PostAsync(method.RequestUri, content).Result;
                WriteResponse(method, response);
                Console.ReadLine();
            }
            return Post;
        }

        private Action CreatePutAction(ApiMethodDTO method)
        {
            void Put()
            {
                object content = ReadFromBodyParameter(method.Parameters.First());
                var response = restService.PutAsync(method.RequestUri, content).Result;
                WriteResponse(method, response);
                Console.ReadLine();
            };
            return Put;
        }

        private ApiInterfaceMapDTO GetApiInterfaceMap()
        {
            string jsonString = restService.GetAsync(apiInterfaceMapUri).Result
                              .Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ApiInterfaceMapDTO>(jsonString);
        }

        private Type GetType(string assemblyQTypeName)
        {
            if (!types.ContainsKey(assemblyQTypeName))
            {
                types[assemblyQTypeName] = Type.GetType(assemblyQTypeName, true);
            }

            return types[assemblyQTypeName];
        }

        private object ReadFromBodyParameter(ApiParameterDTO parameter,Func<PropertyInfo,bool> ignore=null)
        {
            Type parameterType = GetType(parameter.AssemblyQTypeName);
            if (Readers.Contains(parameterType))
            {
                return Readers[parameterType].Read(ignore);
            }
            else throw new NotSupportedException();
        }

        private void WriteResponse(ApiMethodDTO method, HttpResponseMessage response)
        {
            Console.WriteLine($"statuscode: {response.StatusCode}");
            string jsonString = response.Content.ReadAsStringAsync().Result;
            Type returnType = GetType(method.AssemblyQReturnType);

            if (response.StatusCode == System.Net.HttpStatusCode.OK &&
                returnType != typeof(void) &&
               Writers.Contains(returnType))
            {
                Writers[returnType].Write(ModelJsonSerializer.Deserialize(jsonString, returnType));
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