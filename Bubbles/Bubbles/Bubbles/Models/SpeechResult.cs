using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bubbles.Models
{
    public partial class SpeechResult
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("lexical")]
        public string Lexical { get; set; }

        [JsonProperty("properties")]
        public OtherProperties Properties { get; set; }

        [JsonProperty("confidence")]
        public string Confidence { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("scenario")]
        public string Scenario { get; set; }
    }

    public partial class OtherProperties
    {
        [JsonProperty("HIGHCONF")]
        public string HIGHCONF { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("scenario")]
        public string Scenario { get; set; }

        [JsonProperty("lexical")]
        public string Lexical { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("HIGHCONF")]
        public string HIGHCONF { get; set; }

        [JsonProperty("requestid")]
        public string Requestid { get; set; }
    }

    public partial class SpeechResult
    {
        public static SpeechResult FromJson(string json) => JsonConvert.DeserializeObject<SpeechResult>(json, Converter.Settings);
    }

    public static class Serializes
    {
        public static string ToJson(this SpeechResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converters
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
