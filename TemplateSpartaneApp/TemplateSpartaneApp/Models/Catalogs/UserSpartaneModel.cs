using System.Collections.Generic;
using Newtonsoft.Json;
using TemplateSpartaneApp.Abstractions;

namespace TemplateSpartaneApp.Models.Catalogs
{
    public partial class SpartanUserList : ModelBase
    {
        [JsonProperty("Spartan_Users")]
        public List<UserSpartaneModel> SpartanUsers { get; set; }

        [JsonProperty("RowCount")]
        public int RowCount { get; set; }
    }

    public partial class UserSpartaneModel : ModelBase
    {
        [JsonProperty("Id_User")]
        public int IdUser { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Role")]
        public int? Role { get; set; }

        [JsonProperty("Image")]
        public int? Image { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Status")]
        public int? Status { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
