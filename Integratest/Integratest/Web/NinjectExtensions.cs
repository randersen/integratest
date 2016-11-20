using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace Integratest.Web
{
    public interface  INinjectWireup : INinjectModule { };
    public static class NinjectExtensions
    {
        public static void LoadAllWireups(this IKernel kernel)
        {
            var wireups = GetAllWireups();
            kernel.Load(wireups);
        }

        private static IEnumerable<INinjectWireup> GetAllWireups()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetExportedTypes())
                .Where(IsLoadableWireup)
                .Select(t => Activator.CreateInstance(t) as INinjectWireup);
        }

        private static bool IsLoadableWireup(Type type)
        {
            return typeof(INinjectWireup).IsAssignableFrom(type)
                && !type.IsAbstract
                && !type.IsInterface
                && type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
