using Kosher.Framework;
using System.Reflection;

namespace BA.Repository.Helper
{
    public class DBServiceHelper : Singleton<ServiceProvider>
    {
        private static readonly string RepositorySuffix = "Repository";
        private static readonly Dictionary<Type, Type> _serviceType = new Dictionary<Type, Type>();
        public static void LoadAssembly()
        {
            var assem = Assembly.GetExecutingAssembly();

            foreach (var item in assem.GetTypes())
            {
                if (item.FullName.EndsWith(RepositorySuffix) && item.IsClass == true)
                {
                    Instance.AddTransient(item, item);
                    _serviceType.Add(item, item);
                }
            }
        }
        public static void AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            Instance.AddTransient<TService, TImplementation>();
            _serviceType.Add(typeof(TService), typeof(TImplementation));
        }
        public static void AddSingleton<TService>(TService implementation) where TService : class
        {
            Instance.AddSingleton(implementation);
            _serviceType.Add(typeof(TService), implementation.GetType());
        }
        public static Dictionary<Type, Type> GetServiceTypes()
        {
            return _serviceType;
        }
        public static T GetService<T>() where T : class
        {
            return Instance.GetService<T>();
        }
    }
}
