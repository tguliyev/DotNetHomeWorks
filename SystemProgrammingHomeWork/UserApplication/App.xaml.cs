using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BaseProject;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using SimpleInjector;
using UserApplication.Services;
using UserApplication.Tools;
using UserApplication.ViewModels;

namespace UserApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container = new Container();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetServices();
            SetStartWindow<HomeViewModel>();
        }

        private void SetServices()
        {
            object? pluginService = PluginService.getDataBasePluginInterface();
            if (pluginService != null)
                Container.RegisterSingleton(typeof(IUserService), pluginService.GetType());
            
            Container.RegisterSingleton<IMessenger, StrongReferenceMessenger>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<HomeViewModel>();
            Container.RegisterSingleton<UserViewModel>();
            Container.RegisterSingleton<AddUserViewModel>();

            Container.Verify();
        }

        private void SetStartWindow<T>() where T : ObservableObject
        {
            MainViewModel mainViewModel = Container.GetInstance<MainViewModel>();
            mainViewModel.ActiveViewModel = Container?.GetInstance<T>();


            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            mainWindow.ShowDialog();
        }
    }
}
