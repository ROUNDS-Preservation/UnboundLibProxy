using System.Collections.Generic;
using Data = Unbound.Cards.CardData;

namespace UnboundLib.Cards
{
    public static class CardData
    {
        public static string[] GetCards(int teamId)
        {
            return Data.GetCards(teamId);
        }

        public static void AddCard(int teamId, string cardName)
        {
            Data.AddCard(teamId, cardName);
        }

        public static void Clear()
        {
            Data.Clear();
        }

        public static Dictionary<int, List<string>> GetRaw()
        {
            return Data.GetRaw();
        }
    }
}
