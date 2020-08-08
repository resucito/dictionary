using Dictionary.Shared.Models;
using Dictionary.Shared.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Dictionary.Shared.Tests.Utils
{
    public class AllLanguageSongMatcherTests
    {
        private readonly AllLanguageSongMatcher songMatcher;

        public AllLanguageSongMatcherTests()
        {
            this.songMatcher = new AllLanguageSongMatcher(NullLogger<AllLanguageSongMatcher>.Instance);
        }

        [Theory]        
        [MemberData(nameof(TestData.Invalid), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.Valid), MemberType = typeof(TestData))]
        public async Task Matcher_Should_Match_Song(Song song, string query, bool expected) => 
            Assert.Equal(expected, await this.songMatcher.MatchAsync(song, query));

        internal class TestData
        {
            private const string InvalidQuery = "should never match";

            private static Song Song => new Song
            {
                OriginalTitle = "spanish",
                SpanishTitle = "spanish",
                EnglishTitle = "english"
            };

            public static IEnumerable<object[]> Invalid()
            {
                yield return new object[] { new Song(), string.Empty, false };
                yield return new object[] { new Song(), InvalidQuery, false };
                yield return new object[] { Song, InvalidQuery, false };
            }

            public static IEnumerable<object[]> Valid()
            {
                yield return new object[] { Song, Song.OriginalTitle.ToLower(), true };
                yield return new object[] { Song, Song.SpanishTitle, true };
                yield return new object[] { Song, Song.EnglishTitle, true };
            }
        }
    }
}
