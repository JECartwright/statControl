using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using StatControl.Mvvm.Model.SQL;

namespace StatControl.Services
{
    internal static class SQLDataService
    {
        private static readonly string SQLADR = "server=statcontrol.mysql.database.azure.com;user id=admin007;persistsecurityinfo=True;database=stat_control_schema;Password=HowManyDucks2";
        public static List<SQLWeaponDataModel> GetSQLWeaponData(string SteamID, string BeginingDate, string EndDate)
        {
            List<SQLWeaponDataModel> sQLs = new List<SQLWeaponDataModel>();
            MySqlConnection connection = new MySqlConnection(SQLADR);
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT weapondata.WeaponID,total_kills,total_shots,total_hits,PushDate,weaponindex.WeaponName FROM stat_control_schema.weapondata Left Join weaponindex On weapondata.WeaponID = weaponindex.WeaponID where SteamID = \"{SteamID}\" and PushDate between \"{BeginingDate}\" and \"{EndDate}\" order by PushDate ASC; ", connection);
            MySqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    SQLWeaponDataModel wdm = new SQLWeaponDataModel();
                    wdm.weaponid = dr.GetInt32(0);
                    wdm.weapon_kills = dr.GetInt64(1);
                    wdm.weapon_shots = dr.GetInt64(2);
                    wdm.weapon_hits = dr.GetInt64(3);
                    wdm.date = dr.GetDateTime(4);
                    wdm.weapon_name = dr.GetString(5);
                    sQLs.Add(wdm);
                }
            }
            connection.Close();
            return sQLs;
        }

        public static bool AddNewUser(string ID)
        {
            MySqlConnection connection = new MySqlConnection(SQLADR);
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT SteamID FROM users where SteamID = \"{ID}\";", connection);
            MySqlDataReader dr = command.ExecuteReader();
            List<string> users = new List<string>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    users.Add(dr.GetString(0));
                }
            }
            connection.Close();
            if (users.Count == 0)
            {
                connection.Open();
                MySqlCommand command2 = new MySqlCommand($"insert into users (SteamID) Values (\"{ID}\");", connection);
                command2.ExecuteNonQuery();
                connection.Close();
                return true;
            }            
            return false;
        }
    }
}
