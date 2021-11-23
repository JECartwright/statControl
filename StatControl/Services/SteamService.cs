using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamGameStats;

namespace StatControl.Services
{
    public class SteamService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, GameStatsResponse payload, string rawResponse)> GetStatsAsync(string id)
        {
            return await _restService.GetAsync<GameStatsResponse>($"{_apiPath}?appid=730&key={_apiKey}&steamid={id}");
        }
    }
}
