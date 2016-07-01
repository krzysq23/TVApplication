using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TVProvider.Factory;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using TVProvider.Provider;
using DataLayer.Context;
using System.Data.Entity.Infrastructure.Interception;
using BusinessLogic.Services;
using TVApi.Utils;
using Common.Settings;
using Common.Interceptors;
using Common.Log;

namespace TVApi.Installer
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Classes.FromThisAssembly()
                            .BasedOn<IHttpController>()
                            .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                            .BasedOn<IController>()
                            .LifestylePerWebRequest());

            container.Register(Component.For<IMovieService>().ImplementedBy<MovieService>());
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>());
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            container.Register(Component.For<INotificationsService>().ImplementedBy<NotificationsService>());
            container.Register(Component.For<AbstractFactory>().ImplementedBy<TVProviderFactory>());
            container.Register(Component.For<IContext>().ImplementedBy<Context>());
            container.Register(Component.For<IDbInterceptor>().ImplementedBy<ContextInterceptorLogger>());
            container.Register(Component.For<ILogger>().ImplementedBy<Log4Net>());

            container.Register(Component.For<IAppSettings>().ImplementedBy<WebConfigSettings>());

            container.Register(Component.For<ICompositeProvider>().ImplementedBy<CompositeProvider>());

            container.Register(
                Component.For<ITVData>().ImplementedBy<TraktProvider>(),
                Component.For<ITVData>().ImplementedBy<TmdbProvider>()
            );
        }
    }
}