using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using NLog;
using NLog.Config;
using NLog.Targets;
using TelegramBot.Business.Domain.Core;
using TelegramBot.Business.DTOs.Core;
using TelegramBot.Business.Telegram;
using TelegramBot.Common.Cookies;
using TelegramBot.DataAccess.Core;
using IValidatorFactory = TelegramBot.Business.Services.Validators.Core.IValidatorFactory;


namespace TelegramBot.Business.Bootstrapper.DependencyResolution{


    public class IoC{
        private static class Assemblies{
            private static readonly Assembly Domain = Assembly.GetAssembly(typeof(Entity));

            private static readonly Assembly BusinessDto = Assembly.GetAssembly(typeof(IDto));

            private static readonly Assembly DataAccess = Assembly.GetAssembly(typeof(IDataContextFactory));

           // private static readonly Assembly BusinessContracts = Assembly.GetAssembly(typeof(ContractBase));

            private static readonly Assembly BusinessServices = Assembly.GetAssembly(typeof(IValidatorFactory));

            private static readonly Assembly FluentValidation = Assembly.GetAssembly(typeof(IValidator));

            private static readonly Assembly Telegram = Assembly.GetAssembly(typeof(TelegramListener));

            private static readonly Assembly Common = Assembly.GetAssembly(typeof(ICookie));

            public static IEnumerable<Assembly> GetBusinessAssemblies
            {
                get
                {
                    yield return Domain;
                    yield return DataAccess;
                 //   yield return BusinessContracts;
                    yield return BusinessDto;
                    yield return BusinessServices;
                    yield return Common;
                    yield return FluentValidation;
                    yield return Telegram;
                }
            }
        }

        private static IContainer _container;

        private static ILifetimeScope _lifetimeScope;

        public static ILifetimeScope GetLifetimeScope(){
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope());
        }

        public static ILifetimeScope GetLifetimeScope(object tag){
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope(tag));
        }

        public static IContainer Initialize(){
            if (_lifetimeScope != null){
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assemblies.GetBusinessAssemblies.ToArray()).AsImplementedInterfaces();
             builder.RegisterType<TelegramListener>().AsSelf();
            //  builder.RegisterGeneric(typeof(AzureRedisCache<>)).As(typeof(ICache<>)).SingleInstance();
            //            builder.RegisterGeneric(typeof(Cache<>)).As(typeof(ICache<>)).SingleInstance();

            //Register Concrete Classes for WebHost
            //  builder.RegisterType<AnonymousContract>().AsSelf();
            //builder.RegisterType<UserContract>().AsSelf();
            //builder.RegisterType<ManagementContract>().AsSelf();
            //builder.RegisterType<TripContract>().AsSelf();
            //builder.RegisterType<LoggerContract>().AsSelf();
            //builder.RegisterType<DeviceContract>().AsSelf();
            ConfigureLog();
            _container = builder.Build();
            return _container;
        }
        public static void ConfigureLog()
        {
            var config = new LoggingConfiguration();
           
            var fileTarget = new FileTarget();
            config.AddTarget("Nutrition", fileTarget);
            fileTarget.FileName = @"C:\Log\HiDoctor\${shortdate}.txt";
            fileTarget.Layout = "${longdate}    ${message}";
            fileTarget.Encoding = System.Text.Encoding.UTF8;
            fileTarget.ArchiveAboveSize = 10000000;
            fileTarget.MaxArchiveFiles = 30;
            fileTarget.ArchiveEvery = FileArchivePeriod.Hour;
            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            rule.DisableLoggingForLevel(LogLevel.Info);
            config.LoggingRules.Add(rule);




            var fileTarget2 = new FileTarget();
            config.AddTarget("Nutrition", fileTarget2);
            fileTarget2.FileName = @"C:\Log\HiDoctor\Nutrition.txt";
            fileTarget2.Layout = "${message}";
            fileTarget2.Encoding = System.Text.Encoding.UTF8;
            fileTarget2.ArchiveAboveSize = 10000000;
            //fileTarget.MaxArchiveFiles = 30;
            fileTarget2.ArchiveEvery = FileArchivePeriod.Month;
            var rule2 = new LoggingRule();
            rule2.EnableLoggingForLevel(LogLevel.Info);
            rule2.LoggerNamePattern = "*";
            rule2.Targets.Add(fileTarget2);
            config.LoggingRules.Add(rule2);

            LogManager.Configuration = config;
           // LogManager.ThrowExceptions = true;
        }
    }
}