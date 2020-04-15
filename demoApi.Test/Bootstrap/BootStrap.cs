using CommonServiceLocator;
using demoApi.Test.SDK;
using demoApi.Test.SDK.Logger;
using log4net;
using log4net.Config;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.IO;
using System.Net;
using System.Reflection;

namespace demoApi.Test.Bootstrap
{
    class BootStrap
    {
        private static Container _container;
        private static readonly object SyncRoot = new object();
        
        public static Container Container
        {
            get
            {
                if (_container == null)
                {
                    lock (SyncRoot)
                    {
                        if (_container == null)
                        {
                            _container = InitContainer();
                        }
                    }
                }

                return _container;
            }
        }
        
        private static Container InitContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var adapter = new SimpleInjectorServiceLocatorAdapter(container);

            ServiceLocator.SetLocatorProvider(() => adapter);
            RegisterServices(container);

            return container;
        }

        private static void RegisterServices(Container container)
        {
            container.RegisterSingleton<IRestProvider, RestProvider>();
            container.RegisterSingleton<IHandlerApiSDK, HandlerApiSDK>();

            //logger
            var logRepository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.xml"));
            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            
            container.RegisterInstance(logger);
            container.RegisterSingleton<ILogHandler, LogHandler>();
            //container.Register<CustomLogHandler>(Lifestyle.Scoped);

            container.Verify();
            ServicePointManager.DefaultConnectionLimit = 10000;
        }
    }
}
