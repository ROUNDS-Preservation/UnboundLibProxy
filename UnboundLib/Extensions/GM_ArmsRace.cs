using System;
using System.Runtime.CompilerServices;
using Core = Unbound.Core;

namespace UnboundLib.Extensions
{
    // TODO: figure out what to do with this
    [Serializable]
    public class GM_ArmsRaceAdditionalData
    {
        public int[] previousRoundWinners;
        public int[] previousPointWinners;

        public GM_ArmsRaceAdditionalData()
        {
            previousRoundWinners = new int[] { };
            previousPointWinners = new int[] { };
        }
    }
}
