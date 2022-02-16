using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamGameStats;

namespace StatControl.Services
{
    public class SteamGameStatsService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamGameStatsService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, SteamGameStatsResponse payload, string rawResponse)> GetUserStatsAsync(string id)
        {
            return await _restService.GetAsync<SteamGameStatsResponse>($"{_apiPath}?appid=730&key={_apiKey}&steamid={id}");
        }

    }
}
