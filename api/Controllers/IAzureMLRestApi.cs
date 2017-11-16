using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Controllers
{
    public interface IAzureMlRestApi
    {
        [Post("/workspaces/92feef17e1984d7d9a831bc95f367425/services/34f25a3ab6ea4145928a23b27249e2e7/execute?api-version=2.0&details=true")]
        Task<JObject> GetUserAsync([Body] object body, [Header("Authorization")] string authorization);
    }
}
