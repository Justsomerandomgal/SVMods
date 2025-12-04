using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class ShellShock : AModCard
    {
        #region Required properties
        public override string DisplayName => "Shell Shock";

        public override string Description => "Strike all adjacent tiles, this card gains +1 range for every 10 <b><color=#FFBF00>Artifacts</color></b></nobr> you have.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Artifact") }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Repeated, ComponentTrait.SelectionLess, ComponentTrait.Basic, ComponentTrait.Attack }.ToILCPP();

        // Wild is bad because there is no random targets to select
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { 
                new ShellShockTask()
            }.ToILCPP();
        }
    }
}
