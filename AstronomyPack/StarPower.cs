using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine.Tilemaps;

namespace ModdedPack1
{
    internal class StarPower : AModCard
    {
        #region Required properties
        public override string DisplayName => "Star Power";

        public override string Description => "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 800.\nOn spend, draw a card and give it +1 <b><color=#FFBF00>Repeat</color></b> until played, also make it free until played.\nThis card has +1 <b><color=#FFBF00>Repeat</color></b>.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Repeat, MoreInfoWordName.Free, ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 2;

        public override int RepeatAmount => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.SelectionLess, ComponentTrait.Basic }.ToILCPP();

        // This card has a repeat as-is, so it doesn't need any more, block!
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Repeated }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new StarPowerTask().Convert());
            taskList.Add(new StarcycleTask(8, onSpendList).Convert());
            
            return taskList;
        }
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add(new Selection(new ExtraSelectionCondition(new TargetValue())));

            return selections;
        }
    }
}
