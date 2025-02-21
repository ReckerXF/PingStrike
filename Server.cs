using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;

namespace PingStrike.Server
{
    public class Server : BaseScript
    {
        #region Variables
        // Data
        Dictionary<string, Tuple<int, DateTime>> pingData = new Dictionary<string, Tuple<int, DateTime>>();
        #endregion

        #region Constructor
        public Server()
        {
            Debug.WriteLine($"PingKick loaded! The current ping threshold is {ConfigHandler.pingThreshold}.");
        }
        #endregion

        #region Tick (Main Logic)
        [Tick]
        private async Task CheckPlayerPings()
        {
            await Delay(ConfigHandler.pollingRate);

            foreach (Player player in Players)
            {
                string playerId = player.Handle;
                int ping = GetPlayerPing(playerId);

                if (ping < ConfigHandler.pingThreshold)
                    continue; // Go to next loop iteration.

                Debug.WriteLine($"Ping Alert: {player.Name} has a ping of {ping}!");

                if (pingData.ContainsKey(playerId))
                {
                    Tuple<int, DateTime> plyPingData = pingData[playerId];
                    int strike = plyPingData.Item1;
                    DateTime dateTime = plyPingData.Item2;
                    int newStrike = strike + 1;
                    if (dateTime.AddMinutes(ConfigHandler.refreshRate) < DateTime.Now)
                    {
                        if (strike < ConfigHandler.maxStrikes)
                        {
                            pingData[playerId] = new Tuple<int, DateTime>(newStrike, DateTime.Now);
                            SendStrikeMsg(player, ping, newStrike);
                        }
                        else if (strike == ConfigHandler.maxStrikes)
                        {
                            pingData.Remove(playerId);
                            DropPlayer(playerId, $"You have been kicked for being at or above the ping threshold of {ConfigHandler.pingThreshold} after {ConfigHandler.maxStrikes} strikes.");
                        }
                    }
                    else
                    {
                        pingData.Remove(playerId);
                        pingData.Add(playerId, new Tuple<int, DateTime>(1, DateTime.Now));
                        SendStrikeMsg(player, ping, 1);
                    }
                }
                else
                {
                    pingData.Add(playerId, new Tuple<int, DateTime>(1, DateTime.Now));
                    SendStrikeMsg(player, ping, 1);
                }
            }

            await Task.FromResult(0);
        }
        #endregion

        #region Methods
        private void SendStrikeMsg(Player ply, int ping, int strikeNum)
        {
            var messageData = new
            {
                args = new[] { $"Your ping is currently {ping}. You have strike {strikeNum}/{ConfigHandler.maxStrikes}." },
                tags = new[] { "Ping Checker" },
                channel = "server"
            };
            ply.TriggerEvent("chat:addMessage", messageData);
        }
        #endregion

        #region Events
        [EventHandler("playerDropped")]
        private void OnPlayerDropped([FromSource] Player player, string reason)
        {
            if (!pingData.ContainsKey(player.Handle))
                return;

            pingData.Remove(player.Handle);
        }
        #endregion
    }
}