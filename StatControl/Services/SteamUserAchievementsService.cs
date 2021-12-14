using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamUserAchievements;


namespace StatControl.Services
{
    internal class SteamUserAchievementsService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamUserAchievementsService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, SteamUserAchievementsResponse payload, string rawResponse)> GetUserAchieveAsync(string id)
        {
            return await _restService.GetAsync<SteamUserAchievementsResponse>($"{_apiPath}?appid=730&key={_apiKey}&steamid={id}");
        }
    }
}