using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdedPack1;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class CosmicRays : AModCard
    {
        #region Required properties
        public override string DisplayName => "Cosmic Rays";

        public override string Description => "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 2000.\nOn spend, strike a random invader 4 times and draw a card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();

        // This card doesn't need a mech to be played (only really applicable for keeper)
        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Attack, ComponentTrait.SelectionLess, ComponentTrait.Basic }.ToILCPP();

        // We don't want the chain modifier as it makes this card a slot machine
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Chain }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new StrikeRandomInvaderTask());
            onSpendList.Add(new StrikeRandomInvaderTask());
            onSpendList.Add(new StrikeRandomInvaderTask());
            onSpendList.Add(new StrikeRandomInvaderTask());
            onSpendList.Add(new DrawTopDrawPileTask());
            taskList.Add(new StarcycleTask(20, onSpendList).Convert());

            return taskList;
        }
    }
}
