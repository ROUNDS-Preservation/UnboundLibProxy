using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnboundLib.GameModes;
using Networking = Unbound.Networking;
using Gamemodes = Unbound.Gamemodes;
using static UnboundLib.Networking.Utils.NetworkEventCallbacks;

namespace UnboundLib
{
    public class NetworkEventCallbacks : MonoBehaviourPunCallbacks
    {
        public event NetworkEvent OnJoinedRoomEvent {
            add => Networking.Utils.NetworkEventCallbacks.OnJoinedRoomEvent += value;
            remove => Networking.Utils.NetworkEventCallbacks.OnJoinedRoomEvent -= value;
        }
        public event NetworkEvent OnLeftRoomEvent {
            add => Networking.Utils.NetworkEventCallbacks.OnLeftRoomEvent += value;
            remove => Networking.Utils.NetworkEventCallbacks.OnLeftRoomEvent -= value; 
        }

        public override void OnJoinedRoom()
        {
            typeof(Networking.Utils.NetworkEventCallbacks)
                .GetMethod("OnJoinedRoom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(this, null);
        }

        public override void OnLeftRoom()
        {
            typeof(Networking.Utils.NetworkEventCallbacks)
                .GetMethod("OnJoinedRoom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(this, null);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            typeof(Networking.Utils.NetworkEventCallbacks)
                .GetMethod("OnPlayerLeftRoom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(this, new object[] { otherPlayer });
        }
    }
}