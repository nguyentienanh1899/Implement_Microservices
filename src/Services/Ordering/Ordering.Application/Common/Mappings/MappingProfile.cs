using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterFace(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterFace)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null) { methodInfo.Invoke(instance, new object[] { this }); }

                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterFace).ToList();
                    if (interfaces.Count <= 0) continue;
                    foreach (var @iface in interfaces)
                    {
                        var interfaceMethodInfo = iface.GetMethod(mappingMethodName, argumentTypes);
                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
