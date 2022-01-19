using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Constants
{
    public class ApiConstants
    {


        public const string SteamBaseApiUrl = "https://api.steampowered.com/";
        public const string SteamApiKey = "8858FC26F97BACC3D4BB4C44CA52969F";

        public const string SteamAchieveEndpoint = "ISteamUserStats/GetSchemaForGame/v0002/";
        public const string SteamGameStatEndpoint = "ISteamUserStats/GetUserStatsForGame/v0002/";
        public const string SteamUserProfileSummaryEndpoint = "ISteamUser/GetPlayerSummaries/v0002/";
        public const string SteamUserAchievementsEndpoint = "ISteamUserStats/GetPlayerAchievements/v0001/";
        public const string SteamVanityUrlEndpoint = "ISteamUser/ResolveVanityURL/v0001/";
    }
}
