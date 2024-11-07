using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository;
using UrbanSystem.Data.Repository.Contracts;

namespace UrbanSystem.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            List<Type> typesToExclude = new List<Type>()
    {
        typeof(ApplicationUser)
    };

            List<Type> modelTypes = modelsAssembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                            !t.Name.ToLower().EndsWith("attribute") &&
                            !typesToExclude.Contains(t))
                .ToList();

            foreach (Type type in modelTypes)
            {
                Type repositoryInterface = typeof(IRepository<,>);
                Type repositoryInstanceType = typeof(BaseRepository<,>);

                PropertyInfo idPropInfo = type
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.ToLower() == "id");

                Type[] constructArgs = new Type[2];
                constructArgs[0] = type;

                if (idPropInfo == null)
                {
                    constructArgs[1] = typeof(object);
                }
                else
                {
                    constructArgs[1] = idPropInfo.PropertyType;
                }

                repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                services.AddScoped(repositoryInterface, repositoryInstanceType);
            }
        }
    }
}
