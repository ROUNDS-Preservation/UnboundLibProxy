using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib.Networking;
using static UnboundLib.Networking.NetworkingManager;
using Networking = Unbound.Networking;

namespace UnboundLib
{
    public static class NetworkingManager
    {
        private static RaiseEventOptions raiseEventOptionsAll = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };
        private static RaiseEventOptions raiseEventOptionsOthers = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others
        };
        private static SendOptions reliableSendOptions = new SendOptions
        {
            Reliability = true
        };
        private static SendOptions unreliableSendOptions = new SendOptions
        {
            Reliability = false
        };

        private static Dictionary<string, PhotonEvent> events = new Dictionary<string, PhotonEvent>();
        private static Dictionary<Tuple<Type, string>, MethodInfo> rpcMethodCache = new Dictionary<Tuple<Type, string>, MethodInfo>();

        private static byte ModEventCode = 69;

        public static void RegisterEvent(string eventName, PhotonEvent handler)
        {
            Networking.NetworkingManager.RegisterEvent(eventName, handler);
        }

        public static void RaiseEvent(string eventName, RaiseEventOptions options, params object[] data) {
            Networking.NetworkingManager.RaiseEvent(eventName, options, data);
        }

        public static void RaiseEvent(string eventName, params object[] data)
        {
            RaiseEvent(eventName, raiseEventOptionsAll, data);
        }

        public static void RaiseEventOthers(string eventName, params object[] data)
        {
            RaiseEvent(eventName, raiseEventOptionsOthers, data);
        }

        public static void RPC(Type targetType, string methodName, RaiseEventOptions options, SendOptions sendOptions, params object[] data)
        {
            Networking.NetworkingManager.RPC(targetType, methodName, options, sendOptions, data);
        }

        public static void RPC(Type targetType, string methodName, RaiseEventOptions options, params object[] data)
        {
            RPC(targetType, methodName, options, reliableSendOptions, data);
        }
        
        public static void RPC(Type targetType, string methodName, params object[] data)
        {
            RPC(targetType, methodName, raiseEventOptionsAll, reliableSendOptions, data);
        }

        public static void RPC_Others(Type targetType, string methodName, params object[] data)
        {
            RPC(targetType, methodName, raiseEventOptionsOthers, reliableSendOptions, data);
        }
        public static void RPC_Unreliable(Type targetType, string methodName, params object[] data)
        {
            RPC(targetType, methodName, raiseEventOptionsAll, unreliableSendOptions, data);
        }
        public static void RPC_Others_Unreliable(Type targetType, string methodName, params object[] data)
        {
            RPC(targetType, methodName, raiseEventOptionsOthers, unreliableSendOptions, data);
        }

        public static void OnEvent(EventData photonEvent)
        {
            Networking.NetworkingManager.OnEvent(photonEvent);
        }

        private static MethodInfo GetRPCMethod(Type type, string methodName) {
            MethodInfo GetRPCMethodMethod = typeof(NetworkingManager).GetMethod("GetRPCMethod", BindingFlags.NonPublic | BindingFlags.Static);
            return (MethodInfo)GetRPCMethodMethod.Invoke(null, new object[] { type, methodName });
        }
    }
}
