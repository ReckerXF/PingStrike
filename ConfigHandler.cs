using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using Newtonsoft.Json;
using static PingStrike.Server.Models.Configuration;

namespace PingStrike.Server
{
    internal class ConfigHandler : BaseScript
    {
        #region Config Variables
        /// <summary>
        /// The threshold someone has to hit to receive a ping strike.
        /// </summary>
        /// <returns>int</returns>
        public static int pingThreshold = GetConfig().pingThreshold;

        /// <summary>
        /// How often the script checks all players pings (in ms).
        /// </summary>
        /// <returns>int</returns>
        public static int pollingRate = GetConfig().pollingRate;

        /// <summary>
        /// How long the cooldown is before a player's ping strikes are removed.
        /// </summary>
        /// <returns>int</returns>
        public static int refreshRate = GetConfig().refreshRate;

        /// <summary>
        /// The maximum amount of strikes before the player is kicked for high ping.
        /// </summary>
        /// <returns>int</returns>
        public static int maxStrikes = GetConfig().maxStrikes;
        #endregion

        #region Config.Json Handling
        public static Config GetConfig()
        {
            Config data = new Config();
            string jsonFile = API.LoadResourceFile(API.GetCurrentResourceName(), "config.json");

            try
            {
                if (string.IsNullOrEmpty(jsonFile))
                {
                    System.Diagnostics.Debug.WriteLine("The config.json file is empty!");
                }
                else
                {
                    data = JsonConvert.DeserializeObject<Config>(jsonFile);
                }
            }

            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Json Error Reported: {e.Message}\nStackTrace:\n{e.StackTrace}");
            }

            return data;
        }
        #endregion
    }
}
