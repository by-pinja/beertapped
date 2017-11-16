using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestEase;

namespace api.Controllers
{
    public interface IAzureMlRestApi
    {
        [Post("/workspaces/{workspace}/services/{mlServiceId}/execute?api-version=2.0&details=true")]
        Task<JObject> GetUserAsync([Path]string workspace, [Path]string mlServiceId, [Body] object body, [Header("Authorization")] string authorization);
    }
}
