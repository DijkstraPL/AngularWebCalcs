using Autofac;
using Autofac.Core;
using Build_IT_NCalcPlayground.Events;
using Build_IT_NCalcPlayground.Events.Interfaces;
using Build_IT_NCalcPlayground.ViewModels;
using Build_IT_NCalcPlayground.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_NCalcPlayground
{
    public static class ContainerData
    {
        public static IContainer Container { get; private set; }

        internal static void RegisterAppServices()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            Container = containerBuilder.Build();
        }
    }
}
