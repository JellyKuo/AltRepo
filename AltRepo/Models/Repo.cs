using Newtonsoft.Json;

namespace AltRepo.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RepoApp
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("bundleIdentifier")]
        public string? BundleIdentifier { get; set; }

        [JsonProperty("developerName")]
        public string? DeveloperName { get; set; }

        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("versionDate")]
        public DateTime? VersionDate { get; set; }

        [JsonProperty("versionDescription")]
        public string? VersionDescription { get; set; }

        [JsonProperty("downloadURL")]
        public string? DownloadURL { get; set; }

        [JsonProperty("localizedDescription")]
        public string? LocalizedDescription { get; set; }

        [JsonProperty("iconURL")]
        public string? IconURL { get; set; }

        [JsonProperty("tintColor")]
        public string? TintColor { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("screenshotURLs")]
        public List<string>? ScreenshotURLs { get; set; }

        [JsonProperty("permissions")]
        public List<AppPermission>? Permissions { get; set; }

        [JsonProperty("subtitle")]
        public string? Subtitle { get; set; }

        [JsonProperty("versions")]
        public List<AppVersion>? Versions { get; set; }

        [JsonProperty("beta")]
        public bool? Beta { get; set; }
    }

    public class RepoNews
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        [JsonProperty("caption")]
        public string? Caption { get; set; }

        [JsonProperty("tintColor")]
        public string? TintColor { get; set; }

        [JsonProperty("imageURL")]
        public string? ImageURL { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("notify")]
        public bool? Notify { get; set; }

        [JsonProperty("appID")]
        public string? AppID { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }

    public class AppPermission
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("usageDescription")]
        public string? UsageDescription { get; set; }
    }

    public class Repo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("identifier")]
        public string? Identifier { get; set; }

        //[JsonProperty("sourceURL")]
        //public string? SourceURL { get; set; }

        [JsonProperty("apps")]
        public List<RepoApp>? Apps { get; set; }

        [JsonProperty("news")]
        public List<RepoNews>? News { get; set; }

        [JsonProperty("userInfo")]
        public UserInfo? UserInfo { get; set; }
    }

    public class UserInfo
    {
    }

    public class AppVersion
    {
        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("localizedDescription")]
        public string? LocalizedDescription { get; set; }

        [JsonProperty("downloadURL")]
        public string? DownloadURL { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("minOSVersion")]
        public string? MinOSVersion { get; set; }
    }


}
