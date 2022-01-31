using System;
using System.Collections.Generic;
using System.Text;
using StatControl.Services;
using StatControl.Mvvm.Model.SteamUserProfile;
using StatControl.Mvvm.Model.SteamUserAchievements;
using StatControl.Mvvm.Model.SteamGameStats;
using StatControl.Mvvm.Model.SteamAchievementData;
using StatControl.Mvvm.Model.SteamUserFriends;
using System.Diagnostics;

namespace StatControl.Mvvm.Model.ApplicationAPIData
{
    static internal class ApplicatationDataHandler
    {
        private static readonly SteamGameStatsService _steamGameStatsService;
        private static readonly SteamUserAchievementsService _steamUserAchievementsService;
        private static readonly SteamUserProfileService _steamUserProfileService;
        private static readonly SteamAchievementService _steamAchievementDataService;
        private static readonly SteamFriendsService _steamFriendsService;
        public static SteamUserAchievementsResponse resultUserAchieve;
        public static SteamUserProfileResponse resultProfile;
        public static SteamGameStatsResponse resultStats;
        public static SteamAchievementDataResponse resultAchieveData;
        public static SteamFriendsResponse resultFriends;

        public static async void Update(string _steamProfileIdText)
        {
            var resultUserAchieve = await _steamUserAchievementsService.GetUserAchieveAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Achievements Response Received");

            var resultProfile = await _steamUserProfileService.GetUserSummaryAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: User Profile Response Received");

            var resultStats = await _steamGameStatsService.GetUserStatsAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Game Stats Response Received");

            var resultAchieveData = await _steamAchievementDataService.GetAchieveInfoAsync();
            Debug.WriteLine("LOGIN_PAGE: Steam Achievements Response Received");

            var resultFriends = await _steamFriendsService.GetFriendsListAsync(_steamProfileIdText);
            Debug.WriteLine("LOGIN_PAGE: Steam Friends Response Received");
        }
    }
}
