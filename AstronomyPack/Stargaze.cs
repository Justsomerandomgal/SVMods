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

        public override string Description => "On obtain, gain 7500 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> and add a <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Tactical Shooting Star</i></font></b></smallcaps></color></size></voffset> to your deck.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Component, ModContentManager.GetModMoreInfoName("Tactical") }.ToILCPP();

        public override Il2CppCollections.HashSet<CardName> MoreInfoCards => new HashSet<CardName>() { ModContentManager.GetModCardName<ShootingStar>() }.ToILCPP();

        // This function defines when and what this artifact does
        public override void OnObtain(PlayerDataSO playerData)
        {
            base.OnObtain(playerData);
            playerData.starbucksAmount += 75;
            playerData.AddCardToDeck(new PlayerCardData(ModContentManager.GetModCardName<ShootingStar>(), ComponentName.Tactical));
        }
    }
}
