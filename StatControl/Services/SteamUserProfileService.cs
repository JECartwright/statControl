using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamUserProfile;

namespace StatControl.Services
{
    public class SteamUserProfileService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamUserProfileService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, SteamUserProfileResponse payload, string rawResponse)> GetUserSummaryAsync(string id)
        {
            return await _restService.GetAsync<SteamUserProfileResponse>($"{_apiPath}?key={_apiKey}&steamids={id}");

        }
    }
}
