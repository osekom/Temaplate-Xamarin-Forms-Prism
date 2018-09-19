//#define DEVELOPMENT
//#define PRODUCTION
//#define DEMO
#define TESTING

using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace TemplateSpartaneApp.Settings
{
    public static class AppConfiguration
    {
        public static ConfigurationValue Values { get; } = Initialize();
        private static ConfigurationValue Initialize()
        {
            var assembly = typeof(AppConfiguration).GetTypeInfo().Assembly;

#if DEVELOPMENT
            var stream = assembly.GetManifestResourceStream("TemplateSpartaneApp.Settings.Develop.json");
#elif PRODUCTION
            var stream = assembly.GetManifestResourceStream("TemplateSpartaneApp.Settings.Production.json");
#elif DEMO
            var stream = assembly.GetManifestResourceStream("TemplateSpartaneApp.Settings.Demo.json");
#elif TESTING
            var stream = assembly.GetManifestResourceStream("TemplateSpartaneApp.Settings.Testing.json");
#endif
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<ConfigurationValue>(reader.ReadToEnd());
            }
        }
    }

    [Preserve(AllMembers = true)]
    public class ConfigurationValue
    {
        public string BaseUrl { get; set; }
        public string BaseUrlFiles { get; set; }
        public string UserToken { get; set; }
        public string PassToken { get; set; }
        public string UrlToken { get; set; }
        public string NameDB { get; set; }
    }
}
