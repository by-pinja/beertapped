using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Common
{
    public interface IUntappdRestApi
    {
        [Get("/v4/user/beers/{untappdUser}?access_token={apiKey}&offset={offset}&limit=50")]
        Task<JObject> GetUserAsync([Path("untappdUser")] string untappdUser, [Path("apiKey")] string apiKey, [Path("offset")] int offset);
    }
}
