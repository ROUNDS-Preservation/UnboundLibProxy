using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnboundLib.Networking.Utils.PingMonitor;
using Networking = Unbound.Networking;

namespace UnboundLib
{
    [DisallowMultipleComponent]
    public class PingMonitor : MonoBehaviourPunCallbacks
    {
        public Dictionary<int, bool> ConnectedPlayers
        {
            get => Networking.Utils.PingMonitor.instance.ConnectedPlayers;
            set => Networking.Utils.PingMonitor.instance.ConnectedPlayers = value;
        }
        public Dictionary<int, int> PlayerPings
        {
            get => Networking.Utils.PingMonitor.instance.PlayerPings;
            set => Networking.Utils.PingMonitor.instance.PlayerPings = value;
        }
        public Action<int, int> PingUpdateAction
        {
            get => Networking.Utils.PingMonitor.instance.PingUpdateAction;
            set => Networking.Utils.PingMonitor.instance.PingUpdateAction = value;
        }
        public static PingMonitor instance;
        private int pingUpdate;

        private void Start()
        {
            // This was removed because its functionality was moved to UnboundNetworking, and the method will
            // be removed in a future update
        }

        private void Awake()
        {
            // This was removed because its functionality was moved to UnboundNetworking, and the method will
            // be removed in a future update
        }

        private void FixedUpdate()
        {
            // This was removed because its functionality was moved to UnboundNetworking, and the method will
            // be removed in a future update
        }

        public override void OnJoinedRoom()
        {
            Networking.Utils.PingMonitor.instance.OnJoinedRoom();
        }

        public override void OnLeftRoom()
        {
            Networking.Utils.PingMonitor.instance.OnLeftRoom();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            Networking.Utils.PingMonitor.instance.OnPlayerLeftRoom(otherPlayer);
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Networking.Utils.PingMonitor.instance.OnPlayerEnteredRoom(newPlayer);
        }

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
        {
            Networking.Utils.PingMonitor.instance.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }

        /// <summary>
        /// Check the players to see which ones are controlled by a specific actor number.
        /// </summary>
        /// <param name="actorNumber">Actor number to check for.</param>
        /// <returns>An array of players who are owned by the actor number. Returns null if none are found.</returns>
        public Player[] GetPlayersByOwnerActorNumber(int actorNumber)
        {
            return Networking.Utils.PingMonitor.instance.GetPlayersByOwnerActorNumber(actorNumber);
        }

        public PingColor GetPingColors(int ping)
        {
            return Networking.Utils.PingMonitor.instance.GetPingColors(ping);
        }
    }
}
