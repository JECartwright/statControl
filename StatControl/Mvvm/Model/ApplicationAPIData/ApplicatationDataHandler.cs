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
using System.Threading.Tasks;

namespace StatControl.Mvvm.Model.ApplicationAPIData
{
    internal static class ApplicatationDataHandler
    {
        public static string MainUserId;
        private static SteamGameStatsService _steamGameStatsService;
        private static SteamUserAchievementsService _steamUserAchievementsService;
        private static SteamUserProfileService _steamUserProfileService;
        private static SteamAchievementService _steamAchievementDataService;
        private static SteamFriendsService _steamFriendsService;
        public static SteamUserAchievementsResponse resultUserAchieve;
        public static SteamUserProfileResponse resultProfile;
        public static SteamGameStatsResponse resultStats;
        public static SteamAchievementDataResponse resultAchieveData;
        public static SteamFriendsResponse resultFriends;
        public static string currentID;

        public static async Task ReloadMain()
        {
            await Update(MainUserId);
            currentID = MainUserId;
        }


        /// <summary>
        /// Update The API Data Can Be Used To Load New Users Data
        /// </summary>
        /// <param name="steamProfileIdText">Steam ID</param>
        public static async Task Update(string steamProfileIdText)
        {
            var _resultUserAchieve = await _steamUserAchievementsService.GetUserAchieveAsync(steamProfileIdText);
            resultUserAchieve = _resultUserAchieve.payload;
            Debug.WriteLine("LOGIN_PAGE: User Achievements Response Received");

            var _resultProfile = await _steamUserProfileService.GetUserSummaryAsync(steamProfileIdText);
            resultProfile = _resultProfile.payload;
            Debug.WriteLine("LOGIN_PAGE: User Profile Response Received");

            var _resultStats = await _steamGameStatsService.GetUserStatsAsync(steamProfileIdText);
            resultStats = _resultStats.payload;
            Debug.WriteLine("LOGIN_PAGE: Game Stats Response Received");

            var _resultAchieveData = await _steamAchievementDataService.GetAchieveInfoAsync();
            resultAchieveData = _resultAchieveData.payload;
            Debug.WriteLine("LOGIN_PAGE: Steam Achievements Response Received");

            var _resultFriends = await _steamFriendsService.GetFriendsListAsync(steamProfileIdText);
            resultFriends = _resultFriends.payload;
            Debug.WriteLine("LOGIN_PAGE: Steam Friends Response Received");
            CheckAPI = false;
            
            //Checking to see if the response was successful
            if (_resultUserAchieve.status == 0 & _resultAchieveData.status == 0 & _resultProfile.status == 0 &
                _resultStats.status == 0 & _resultFriends.status == 0)
            {
                if (_resultStats.payload.playerstats.stats != null & _resultUserAchieve.payload.playerstats != null &
                    _resultProfile.payload.response.players != null & _resultAchieveData.payload.game != null &
                    _resultFriends.payload.friendslist.friends != null)
                {
                    CheckAPI = true;
                    currentID = steamProfileIdText;
                }
                else
                {
                    Debug.WriteLine("Data Returned is null.");
                }
            }
            else
            {
                Debug.WriteLine("Error in getting API.");
            }
        }

        /// <summary>
        /// Used To Collect The Services To The API (ONLY USE FROM LOGIN)
        /// </summary>
        /// <param name="steamGameStatsService">Game Data Service</param>
        /// <param name="steamUserAchievementsService">User Achievements Service</param>
        /// <param name="steamUserProfileService">User Profile Service</param>
        /// <param name="steamAchievementService">Achivement Data Service</param>
        /// <param name="steamFriendsService">User Friends Service</param>
        public static void SetService(SteamGameStatsService steamGameStatsService, SteamUserAchievementsService steamUserAchievementsService, SteamUserProfileService steamUserProfileService, SteamAchievementService steamAchievementService, SteamFriendsService steamFriendsService)
        {
            _steamGameStatsService = steamGameStatsService;
            _steamUserAchievementsService = steamUserAchievementsService;
            _steamUserProfileService = steamUserProfileService;
            _steamAchievementDataService = steamAchievementService;
            _steamFriendsService = steamFriendsService;
        }

        /// <summary>
        /// To Get Services To The Social Page
        /// </summary>
        /// <returns>Friends Service</returns>
        public static SteamUserProfileService GetUserProfileServiceForSocial()
        {
            return _steamUserProfileService;
        }

        /// <summary>
        /// Weather The API Worked
        /// </summary>
        public static bool CheckAPI;
    }
}
