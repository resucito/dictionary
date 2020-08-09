using Dictionary.Shared.Models;
using System.Threading.Tasks;

namespace Dictionary.Shared.Interfaces
{
    public interface ISongMatcher
    {
        Task<bool> MatchAsync(Song song, string query);
    }
}
