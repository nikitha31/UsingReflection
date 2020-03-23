using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UsingReflection
{
    public class GetNames
    {
        public List<String> getDetails()
        {
            Assembly asm = Assembly.GetExecutingAssembly();


            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && method.ReturnType == typeof(ActionResult)).ToList();

            var list = new List<String>();
            foreach (var item in controlleractionlist)
            {
                var parameters = item.GetParameters();

                string url = item.DeclaringType.Name.Substring(0, item.DeclaringType.Name.Length - 10) + "/" + item.Name;

                if (parameters.Length > 0)
                {
                    foreach (var para in parameters)
                        url = url + "/" + para.Name;
                }
                list.Add(url);
            }



            return list;

        }
    }
}