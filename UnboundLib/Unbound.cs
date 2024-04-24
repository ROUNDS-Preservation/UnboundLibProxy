using BepInEx;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Configuration;
using Jotunn.Utils;
using TMPro;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using UnboundLib.Utils;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unbound.Core;
using Unbound.Networking;
using Unbound.Gamemodes;
using System.Reflection;
using Unbound.Cards;
using Unbound.Core.Utils;

namespace UnboundLib
{
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class Unbound : BaseUnityPlugin
    {
        private const string ModId = "com.willis.rounds.unbound";
        private const string ModName = "Rounds Unbound";
        public const string Version = "3.2.13";

        public static UnboundCore Instance { get { return UnboundCore.Instance; } }
        public static readonly ConfigFile config = new ConfigFile(Path.Combine(Paths.ConfigPath, "UnboundLib.cfg"), true);

        public Canvas canvas
        {
            get
            {
                return UnboundCore.Instance.canvas;
            }
        }

        [Obsolete("This should not be used anymore instead use CardManager.defaultCards")]
        internal static CardInfo[] defaultCards => CardManager.defaultCards;
        [Obsolete("This should not be used anymore instead use CardManager.activeCards")]
        internal static List<CardInfo> activeCards => CardManager.activeCards.ToList();
        [Obsolete("This should not be used anymore instead use CardManager.inactiveCards")]
        internal static List<CardInfo> inactiveCards => CardManager.inactiveCards;

        public delegate void OnJoinedDelegate();
        public delegate void OnLeftDelegate();
        public static event OnJoinedDelegate OnJoinedRoom;
        public static event OnLeftDelegate OnLeftRoom;

        internal static List<string> loadedGUIDs = new List<string>();
        internal static List<string> loadedMods = new List<string>();
        internal static List<string> loadedVersions = new List<string>();

        internal static List<Action> handShakeActions = new List<Action>();

        public static Dictionary<string, bool> lockInputBools
        {
            get {
                return UnboundCore.lockInputBools;
            }
        }

        internal static AssetBundle UIAssets;
        public static AssetBundle toggleUI { get { return UnboundCore.toggleUI; } set { UnboundCore.toggleUI = toggleUI; } }
        internal static AssetBundle linkAssets;

        public Unbound()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        private static IEnumerator CloseLobby(IGameModeHandler gm)
        {
            // this *SHOULD* work, if it doesnt, then idk
            MethodInfo method = typeof(UnboundGamemodes).GetMethod("CloseLobby", BindingFlags.NonPublic | BindingFlags.Static);
            yield return method.Invoke(null, new object[] { gm });
            //yield return UnboundCore.Instance.StartCoroutine(method);
        }

        private void Awake()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        private void Start()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        private void Update()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        private void OnGUI()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        private static void LoadAssets()
        {
            // This was removed because its functionality was moved to UnboundCore, and other Assemblies, and the method will
            // be removed in a future update
        }

        [UnboundRPC]
        public static void BuildInfoPopup(string message)
        {
            UnboundCore.BuildInfoPopup(message);
        }

        [UnboundRPC]
        public static void BuildModal(string title, string message)
        {
            UnboundCore.BuildModal(title, message);
        }
        public static ModalHandler BuildModal()
        {
            return UnboundCore.BuildModal();
        }
        public static void RegisterCredits(string modName, string[] credits = null, string[] linkTexts = null, string[] linkURLs = null)
        {
            UnboundCore.RegisterCredits(modName, credits, linkTexts, linkURLs);
        }

        public static void RegisterMenu(string name, UnityAction buttonAction, Action<GameObject> guiAction, GameObject parent = null)
        {
            UnboundCore.RegisterMenu(name, buttonAction, guiAction, parent);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static void RegisterMenu(string name, UnityAction buttonAction, Action<GameObject> guiAction, GameObject parent = null, bool showInPauseMenu = false)
        {
            UnboundCore.RegisterMenu(name, buttonAction, guiAction, parent, showInPauseMenu);
        }

        public static void RegisterGUI(string modName, Action guiAction)
        {
            UnboundCore.RegisterGUI(modName, guiAction);
        }

        public static void RegisterCredits(string modName, string[] credits = null, string linkText = "", string linkURL = "")
        {
            UnboundCore.RegisterCredits(modName, credits, linkText, linkURL);
        }

        public static void RegisterClientSideMod(string GUID)
        {
            UnboundNetworking.RegisterClientSideMod(GUID);
        }
        public static void AddAllCardsCallback(Action<CardInfo[]> callback)
        {
            UnboundCards.AddAllCardsCallback(callback);
        }

        public static void RegisterHandshake(string modId, Action callback)
        {
            UnboundNetworking.RegisterHandshake(modId, callback);
        }

        #region Remove these at a later date when mod's have updated to LevelManager
        [Obsolete("This method is obsolete. Use LevelManager.RegisterMaps() instead.", false)]
        public static void RegisterMaps(AssetBundle assetBundle)
        {
            LevelManager.RegisterMaps(assetBundle);
        }

        [Obsolete("This method is obsolete. Use LevelManager.RegisterMaps() instead.", false)]
        public static void RegisterMaps(IEnumerable<string> paths)
        {
            RegisterMaps(paths, "Modded");
        }

        [Obsolete("This method is obsolete. Use LevelManager.RegisterMaps() instead.", false)]
        public static void RegisterMaps(IEnumerable<string> paths, string categoryName)
        {
            LevelManager.RegisterMaps(paths);
        }
        #endregion

        public static bool IsNotPlayingOrConnected()
        {
            return UnboundCore.IsNotPlayingOrConnected();
        }

        internal static readonly ModCredits modCredits = new ModCredits("UNBOUND Proxy", new[]
        {
            "Willis, Tilastokeskus, Pykess, Ascyst, Boss Sloth Inc., willuwontu, otDan (Original contributers)",
            "Scyye (Proxy Update)"
        }, 
            new[] { "New GitHub", "Old GitHub", "Proxy GitHub"},
            new[] { "https://github.com/Rounds-Preservation/UnboundLib", "https://github.com/Rounds-Modding/UnboundLib" 
            "https://github.com/ROUNDS-Preservation/UnboundLibProxy"});
    }
}