using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Linq;
using UnboundLib.Utils;
using Unbound.Cards;
using Custom = Unbound.Cards.CustomCard;

namespace UnboundLib.Cards
{
    public abstract class CustomCard : MonoBehaviour
    {
        public static List<CardInfo> cards = new List<CardInfo>();

        public CardInfo cardInfo;
        public Gun gun;
        public ApplyCardStats cardStats;
        public CharacterStatModifiers statModifiers;
        public Block block;
        private bool isPrefab;

        private void Awake()
        {
            // This was removed because its functionality was moved to UnboundCards, and the method will
            // be removed in a future update
        }

        private void Start()
        {
            // This was removed because its functionality was moved to UnboundCards, and the method will
            // be removed in a future update
        }

        protected abstract string GetTitle();
        protected abstract string GetDescription();
        protected abstract CardInfoStat[] GetStats();
        protected abstract CardInfo.Rarity GetRarity();
        protected abstract GameObject GetCardArt();
        protected abstract CardThemeColor.CardThemeColorType GetTheme();
        protected virtual GameObject GetCardBase()
        {
            return UnboundCards.templateCard.cardBase;
        }

        public virtual void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers) { }

        public virtual void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            SetupCard(cardInfo, gun, cardStats, statModifiers);
        }

        public abstract void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats);

        public virtual void OnReassignCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            OnReassignCard();
        }
        public virtual void OnReassignCard()
        { }

        public virtual void OnRemoveCard() { }

        public virtual void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            OnRemoveCard();
        }

        /// <summary>
        /// Returns if the card should be enabled when built. Cards that are not enabled do not appear in the Toggle Cards menu, nor can be spawned in game by any regular means
        /// </summary>
        public virtual bool GetEnabled()
        {
            return true;
        }

        /// <summary>
        /// Returns the name of the mod this card is from. Should be unique.
        /// </summary>
        public virtual string GetModName()
        {
            return "Modded";
        }

        /// <summary>
        /// A callback method that is called each time the card is spawned in and fully instantiated
        /// </summary>
        public virtual void Callback()
        {

        }

        public static void BuildCard<T>() where T : Custom
        {
            BuildCard<T>(null);
        }

        public static void BuildCard<T>(Action<CardInfo> callback) where T : Custom
        {
            Custom.BuildCard<T>(callback);
        }

        public static void BuildUnityCard<T>(GameObject cardPrefab, Action<CardInfo> callback) where T : Custom
        {
            Custom.BuildUnityCard<T>(cardPrefab, callback);
        }

        // TODO
        public void BuildUnityCard(Action<CardInfo> callback)
        {
            CardInfo cardInfo = this.gameObject.GetComponent<CardInfo>();
            CustomCard customCard = this;
            GameObject cardPrefab = this.gameObject;

            cardInfo.cardBase = customCard.GetCardBase();
            cardInfo.cardStats = customCard.GetStats();
            cardInfo.cardName = customCard.GetTitle();
            cardInfo.gameObject.name = $"__{customCard.GetModName()}__{customCard.GetTitle()}".Sanitize();
            cardInfo.cardDestription = customCard.GetDescription();
            cardInfo.sourceCard = cardInfo;
            cardInfo.rarity = customCard.GetRarity();
            cardInfo.colorTheme = customCard.GetTheme();
            cardInfo.cardArt = customCard.GetCardArt();

            PhotonNetwork.PrefabPool.RegisterPrefab(cardInfo.gameObject.name, cardPrefab);

            if (customCard.GetEnabled())
            {
                CardManager.cards.Add(cardInfo.gameObject.name, new Card(customCard.GetModName().Sanitize(), Unbound.BindConfig("Cards: " + customCard.GetModName().Sanitize(), cardInfo.gameObject.name, true), cardInfo));
            }

            this.Awake();

            Unbound.Instance.ExecuteAfterFrames(5, () =>
            {
                callback?.Invoke(cardInfo);
            });
        }

        public static void RegisterUnityCard<T>(GameObject cardPrefab, Action<CardInfo> callback) where T : Custom
        {
            Custom.RegisterUnityCard<T>(cardPrefab, callback);
        }

        public static void RegisterUnityCard(GameObject cardPrefab, string modInitials, string cardname, bool enabled, Action<CardInfo> callback)
        {
            Custom.RegisterUnityCard(cardPrefab, modInitials, cardname, enabled, callback);
        }

        // TODO 
        public void RegisterUnityCard(Action<CardInfo> callback)
        {
            CardInfo cardInfo = this.gameObject.GetComponent<CardInfo>();

            cardInfo.gameObject.name = $"__{this.GetModName()}__{this.GetTitle()}".Sanitize();

            PhotonNetwork.PrefabPool.RegisterPrefab(cardInfo.gameObject.name, this.gameObject);

            if (this.GetEnabled())
            {
                CardManager.cards.Add(cardInfo.gameObject.name, new Card(this.GetModName().Sanitize(), Unbound.BindConfig("Cards: " + this.GetModName().Sanitize(), cardInfo.gameObject.name, true), cardInfo));
            }

            Unbound.Instance.ExecuteAfterFrames(5, () =>
            {
                callback?.Invoke(cardInfo);
            });
        }

        private static void ResetOnlyGunStats(Gun gun)
        {
            typeof(Custom).GetMethod("ResetGunStats", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { gun });
        }

        private static void ResetOnlyCharacterStatModifiersStats(CharacterStatModifiers characterStatModifiers)
        {
            typeof(Custom).GetMethod("ResetCharacterStatModifiersStats", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { characterStatModifiers });
        }

        private static void ResetOnlyBlockStats(Block block)
        {
            typeof(Custom).GetMethod("ResetBlockStats", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { block });
        }
    }
}
