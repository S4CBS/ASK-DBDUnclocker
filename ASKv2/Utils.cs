﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKv2
{
    internal class Utils
    {
        private static string ProfilePath = Path.Combine(Program.ProfilePath, "Bloodweb.json");
        private static string ProfilePath2 = Path.Combine(Program.ProfilePath, "Profile.json");
        public static void updatePrestigeLvL(string lvl)
        {
            int level = int.Parse(lvl);
            // Обновление Bloodweb.json
            if (Directory.Exists(Program.ProfilePathASK))
            {
                string JSON = File.ReadAllText(ProfilePath);
                var SettingsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSON);

                if (SettingsObj.ContainsKey("prestigeLevel"))
                {
                    SettingsObj["prestigeLevel"] = level;
                }

                string FinalJson = JsonConvert.SerializeObject(SettingsObj, Formatting.Indented);
                File.WriteAllText(ProfilePath, FinalJson);
            } else
            {
                MessageBox.Show("Файл не найден.", "Изменение престижа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1.signal = true;
            }
            // Обновление Profile.json
            if (Directory.Exists(Program.ProfilePathASK))
            {
                string JSON = File.ReadAllText(ProfilePath2);
                var SettingsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSON);

                if (SettingsObj.ContainsKey("list"))
                {
                    var listArray = SettingsObj["list"] as JArray;

                    if (listArray != null)
                    {
                        foreach ( var item in listArray )
                        {
                            if (item["prestigeLevel"] != null)
                            {
                                item["prestigeLevel"] = level;
                            }
                        }
                    }
                }

                string FinalJson = JsonConvert.SerializeObject(SettingsObj, Formatting.Indented);
                File.WriteAllText(ProfilePath2, FinalJson);
            }
        }

        public static string InitializeLvLPrestige()
        {
            string JSON = File.ReadAllText(ProfilePath);
            var SettingsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSON);

            if (SettingsObj.ContainsKey("prestigeLevel"))
            {
                var prestigeLvL = SettingsObj["prestigeLevel"].ToString();

                if (!string.IsNullOrEmpty(prestigeLvL)) {
                    return prestigeLvL;
                }
            }
            return "0";
        }
        public static string ReturnProfile()
        {
            string JSON = File.ReadAllText(Program.CfgProfilePath);
            var SettingsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSON);

            if (SettingsObj != null && SettingsObj.ContainsKey("profile"))
            {
                return SettingsObj["profile"].ToString();
            }
            return "";
        }
        public static void SetProfile(string profile)
        {
            string JSON = File.ReadAllText(Program.CfgProfilePath);
            var SettingsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(JSON);

            if (SettingsObj != null && SettingsObj.ContainsKey("profile"))
            {
                SettingsObj["profile"] = profile;
            }
            string FinalJson = JsonConvert.SerializeObject(SettingsObj);
            File.WriteAllText(Program.CfgProfilePath, FinalJson);
        }
    }
}
