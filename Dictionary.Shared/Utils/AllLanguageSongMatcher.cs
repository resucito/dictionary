using Dictionary.Shared.Interfaces;
using Dictionary.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionary.Shared.Utils
{
    public class AllLanguageSongMatcher : ISongMatcher
    {        
        private readonly ILogger<AllLanguageSongMatcher> logger;
        private readonly CompareInfo compare = CultureInfo.InvariantCulture.CompareInfo;

        public AllLanguageSongMatcher(ILogger<AllLanguageSongMatcher> logger)
        {
            this.logger = logger;
        }

        private IDictionary<SongLanguage, string> GetSongTitles(Song song) => new Dictionary<SongLanguage, string>
        {
            { SongLanguage.Default, song.SpanishTitle },
            { SongLanguage.Spanish, song.SpanishTitle },
            { SongLanguage.English, song.EnglishTitle }
        };

        public async Task<bool> MatchAsync(Song song, string query)
        {
            var titles = GetSongTitles(song);
            var tasks = titles.Select(o => IsMatchAsync(o.Value, query));
            var matches = await Task.WhenAll(tasks);
            return matches.Any(o => o);
        }

        private async Task<bool> IsMatchAsync(string title, string query) => 
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(title)) return false;
                return this.compare.IndexOf(title, query, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) > -1;
            });
    }
}
