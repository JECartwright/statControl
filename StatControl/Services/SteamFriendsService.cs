using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StatControl.Services.Rest;
using StatControl.Mvvm.Model.SteamUserFriends;

namespace StatControl.Services
{
    internal class SteamFriendsService
    {
        private readonly IRestService _restService;
        private readonly string _apiPath;
        private readonly string _apiKey;

        public SteamFriendsService(IRestService restService, string apiPath, string apiKey)
        {
            _restService = restService;
            _apiPath = apiPath;
            _apiKey = apiKey;
        }

        public async Task<(ResultStatus status, Root payload, string rawResponse)> GetFriendsListAsync(string ID)
        {
            return await _restService.GetAsync<Root>($"{_apiPath}?key={_apiKey}&steamid={ID}&relationship=friend&format=json");
        }
    }
}
