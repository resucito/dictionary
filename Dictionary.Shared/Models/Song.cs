using System.Text.Json.Serialization;

namespace Dictionary.Shared.Models
{
    public class Song
    {
        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }
        [JsonPropertyName("spanish_title")]
        public string SpanishTitle { get; set; }
        [JsonPropertyName("english_title")]
        public string EnglishTitle { get; set; }
    }
}
