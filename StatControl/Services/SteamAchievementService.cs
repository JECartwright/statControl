using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamAchievementData;

namespace StatControl.Services
{
    internal class SteamAchievementService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamAchievementService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, SteamAchievementDataResponse payload, string rawResponse)> GetAchieveInfoAsync(string id)
        {
            return await _restService.GetAsync<SteamAchievementDataResponse>($"{_apiPath}?key={_apiKey}&appid=730&l=english&format=json");
        }

    }
}
