using System;
using System.Collections.Generic;
using System.Text;

namespace StatControl.Mvvm.Model.SteamUserProfile
{
    public class UserModel
    {
        public UserModel(string steamid, int communityvisibilitystate, int profilestate, string personaname, string realname, int commentpermission, string profileurl, string avatar, string avatarmedium, string avatarfull, string avatarhash, int lastlogoff, int personastate, string primaryclanid, int timecreated, int personastateflags)
        {
            this.steamid = steamid;
            this.communityvisibilitystate = communityvisibilitystate;
            this.profilestate = profilestate;
            this.personaname = personaname;
            this.realname = realname;
            this.commentpermission = commentpermission;
            this.profileurl = profileurl;
            this.avatar = avatar;
            this.avatarmedium = avatarmedium;
            this.avatarfull = avatarfull;
            this.avatarhash = avatarhash;
            this.lastlogoff = lastlogoff;
            this.personastate = personastate;
            this.primaryclanid = primaryclanid;
            this.timecreated = timecreated;
            this.personastateflags = personastateflags;
        }

        public string steamid { get; private set; }
        public int communityvisibilitystate { get; private set; }
        public int profilestate { get; private set; }
        public string personaname { get; private set; }
        public string realname { get; private set; }
        public int commentpermission { get; private set; }
        public string profileurl { get; private set; }
        public string avatar { get; private set; }
        public string avatarmedium { get; private set; }
        public string avatarfull { get; private set; }
        public string avatarhash { get; private set; }
        public int lastlogoff { get; private set; }
        public int personastate { get; private set; }
        public string primaryclanid { get; private set; }
        public int timecreated { get; private set; }
        public int personastateflags { get; private set; }
    }
}
