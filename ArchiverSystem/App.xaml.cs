using ArchiverSystem.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiverSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ResourceDictionary resDic = new ResourceDictionary
            {
                Source = new Uri($"./Resources/Languages/StringResource-eng.xaml", UriKind.Relative)
            };        
            Current.Resources.MergedDictionaries.Add(resDic);
        }
    }
}
