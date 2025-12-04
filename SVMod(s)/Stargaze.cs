using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdedPack1;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace ModdedPack1
{
    internal class Stargaze : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Stargaze";

        public override string Description => "On obtain, gain 7500 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> and add a <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Shooting Star</i></font></b></smallcaps></color></size></voffset> to your deck.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // This function defines when and what this artifact does
        public override void OnObtain(PlayerDataSO playerData)
        {
            base.OnObtain(playerData);
            playerData.starbucksAmount += 75;
            playerData.AddCardToDeck(new PlayerCardData(ModContentManager.GetModCardName<ShootingStar>()));
        }
    }
}
