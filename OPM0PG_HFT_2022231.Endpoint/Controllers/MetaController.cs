using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private const string Domain = "http://localhost:15486/api/";
        private static readonly ApiInterfaceMapDTO ApiInterfaceMap = new ApiInterfaceMapDTO(MapControllers().ToArray());

        [HttpGet]
        public ApiInterfaceMapDTO GetApiInterfaceMap()
        {
            return ApiInterfaceMap;
        }

        private static HttpMethodType GetHttpMethodType(HttpMethodAttribute attribute)
        {
            if (attribute is HttpGetAttribute)
            {
                return HttpMethodType.GET;
            }
            else if (attribute is HttpPostAttribute)
            {
                return HttpMethodType.POST;
            }
            else if (attribute is HttpPutAttribute)
            {
                return HttpMethodType.PUT;
            }
            else if (attribute is HttpDeleteAttribute)
            {
                return HttpMethodType.DELETE;
            }
            else return HttpMethodType.None;
        }

        private static string GetRequestUri(MethodInfo method)
        {
            return Domain + method.DeclaringType.Name.Replace("Controller", "") + "/" + method.Name + "/";
        }

        private static IEnumerable<ApiControllerDTO> MapControllers()
        {
            string controllerPostFix = "Controller";
            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name.EndsWith(controllerPostFix) && !t.Name.Contains(nameof(MetaController)));
            foreach (var item in controllers)
            {
                yield return new ApiControllerDTO(item.Name.Replace(controllerPostFix, ""), MapMethods(item).ToArray());
            }
        }

        private static IEnumerable<ApiMethodDTO> MapMethods(Type controller)
        {
            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<HttpMethodAttribute>() is HttpMethodAttribute attribute)
                {
                    yield return new ApiMethodDTO(method.Name,
                                                  GetRequestUri(method),
                                                  method.ReturnType.AssemblyQualifiedName,
                                                  GetHttpMethodType(attribute),
                                                  MapParameters(method).ToArray());
                }
            }
        }

        private static IEnumerable<ApiParameterDTO> MapParameters(MethodInfo method)
        {
            foreach (var parameter in method.GetParameters())
            {
                yield return new ApiParameterDTO(parameter.Name, parameter.ParameterType.AssemblyQualifiedName);
            }
        }
    }
}