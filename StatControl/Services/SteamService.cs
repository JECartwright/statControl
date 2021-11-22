using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model;

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

        public async Task<StatModel> GetStatsAsync(string id)
        {
            var result = await _restService.GetAsync<StatModel>($"{_apiPath}?appid=730&apikey={_apiKey}&steamid={id}");

            return result.payload;
        }
    }
}
