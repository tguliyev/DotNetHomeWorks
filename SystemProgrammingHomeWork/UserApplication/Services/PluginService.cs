using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserApplication.Services
{
    internal class PluginService    
    {
        static public object? getDataBasePluginInterface() 
        {
            ObservableCollection<object?> services = new ObservableCollection<object?>();
            var dllFiles = Directory.GetFiles(path: "Plugins").Where(filename => filename.EndsWith(".dll"));

            foreach (var file in dllFiles)
            {
                string absolutePath = $@"{Directory.GetCurrentDirectory()}\{file}";
                Assembly pluginAssembly = Assembly.LoadFile(absolutePath);
                IEnumerable<Type> userServices = from t in pluginAssembly.GetTypes()
                                                 where t.GetInterface("IUserService") != null
                                                 select t;

                var servicesInFile = userServices.Select(t =>
                {
                    return Activator.CreateInstance(t);
                });

                foreach (var service in servicesInFile)
                    services.Add(service);
            }

            return services.FirstOrDefault();
        }
    }
}