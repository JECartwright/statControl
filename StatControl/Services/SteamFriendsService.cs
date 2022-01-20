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

        public async Task<(ResultStatus status, SteamFriendsResponse payload, string rawResponse)> GetFriendsListAsync(string id)
        {
            return await _restService.GetAsync<SteamFriendsResponse>($"{_apiPath}?key={_apiKey}&steamid={id}&relationship=friend&format=json");
        }
    }
}
