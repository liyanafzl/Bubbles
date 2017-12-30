using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bubbles.Models
{
    public partial class ImageResult
    {
        [JsonProperty("categories")]
        public Category[] Categories { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("captions")]
        public Caption[] Captions { get; set; }
    }

    public partial class Caption
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }

    public partial class Color
    {
        [JsonProperty("dominantColorForeground")]
        public string DominantColorForeground { get; set; }

        [JsonProperty("dominantColorBackground")]
        public string DominantColorBackground { get; set; }

        [JsonProperty("dominantColors")]
        public string[] DominantColors { get; set; }

        [JsonProperty("accentColor")]
        public string AccentColor { get; set; }

        [JsonProperty("isBWImg")]
        public bool IsBwImg { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }

    public partial class ImageResult
    {
        public static ImageResult FromJson(string json) => JsonConvert.DeserializeObject<ImageResult>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ImageResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}


    

