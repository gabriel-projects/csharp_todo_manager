using Api.GRRInnovations.TodoManager.Domain.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TodoManager.Domain.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjectionExtension(this IServiceCollection service)
        {
            var ass = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name.Contains("GRRInnovations"));

            foreach (var assemblyName in ass)
            {
                Assembly assembly = Assembly.Load(assemblyName);

                IEnumerable<TypeInfo> definedTypes = assembly.DefinedTypes;

                foreach (TypeInfo item in definedTypes)
                {
                    if (item.IsAbstract)
                    {
                        continue;
                    }

                    object[] customAttributes = item.GetCustomAttributes(typeof(IocAttribute), inherit: false);
                    object[] customAttributes2 = item.GetCustomAttributes(typeof(SingletonAttribute), inherit: false);
                    object[] array = customAttributes;
                    foreach (object obj in array)
                    {
                        IocAttribute iocAttribute = obj as IocAttribute;
                        if (iocAttribute != null)
                        {
                            service.AddScoped(iocAttribute.Interface, item);
                        }
                    }

                    object[] array2 = customAttributes2;
                    foreach (object obj2 in array2)
                    {
                        SingletonAttribute singletonAttribute = obj2 as SingletonAttribute;
                        if (singletonAttribute != null)
                        {
                            service.AddSingleton(singletonAttribute.Interface, item);
                        }
                    }
                }
            }
        }
    }
}
