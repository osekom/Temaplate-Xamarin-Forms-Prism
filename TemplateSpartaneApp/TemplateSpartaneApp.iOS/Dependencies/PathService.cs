using System;
using System.IO;
using TemplateSpartaneApp.Helpers;
using TemplateSpartaneApp.iOS.Dependencies;
using TemplateSpartaneApp.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace TemplateSpartaneApp.iOS.Dependencies
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, AppConfiguration.Values.NameDB);
        }
    }
}
