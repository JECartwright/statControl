using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamVanityUrl;

namespace StatControl.Services
{
    internal class SteamVanityUrlService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamVanityUrlService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, SteamVanityUrlResponse payload, string rawResponse)> GetVanityUrlSummaryAsync(string id)
        {
            return await _restService.GetAsync<SteamVanityUrlResponse>($"{_apiPath}?key={_apiKey}&vanityurl={id}");
        }
    }
}
