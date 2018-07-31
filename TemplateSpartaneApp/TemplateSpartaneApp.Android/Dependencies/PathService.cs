using System;
using System.IO;
using TemplateSpartaneApp.Droid.Dependencies;
using TemplateSpartaneApp.Helpers;
using TemplateSpartaneApp.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace TemplateSpartaneApp.Droid.Dependencies
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, AppConfiguration.Values.NameDB);
        }
    }
}
