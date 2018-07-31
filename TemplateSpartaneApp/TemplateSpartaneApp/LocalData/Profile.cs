using Plugin.Settings.Abstractions;

namespace TemplateSpartaneApp.LocalData
{
    public class Profile
    {
        /// <summary>
        /// Settings service
        /// </summary>
        private ISettings settingsService;

        /// <summary>
        /// Instance
        /// </summary>
        private static Profile instance;

        /// <summary>
        /// Instance
        /// </summary>
        public static Profile Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new Profile();
                }

                return instance;
            }
        }

        /// <summary>
        /// Initialize the app profile data service
        /// </summary>
        /// <param name="settingsService">Settings Service</param>
        public void Initialize(ISettings settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// User Identifier
        /// </summary>
        public int Identifier
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(Identifier)}", default(int));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(Identifier)}", value);
            }
        }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(Email)}", default(string));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(Email)}", value);
            }
        }

        /// <summary>
        /// User Name
        /// </summary>
        public string Name
        {
            get
            {
                return settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(Name)}", default(string));
            }

            set
            {
                settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(Name)}", value);
            }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get => settingsService.GetValueOrDefault($"{nameof(Profile)}{nameof(UserName)}", default(string));
            set => settingsService.AddOrUpdateValue($"{nameof(Profile)}{nameof(UserName)}", value);
        }

        /// <summary>
        /// Clear values
        /// </summary>
        public void ClearValues()
        {
            Identifier = default(int);
            Email = default(string);
            Name = default(string);
        }
    }
}
