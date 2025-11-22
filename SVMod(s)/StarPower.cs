using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class StarPower : AModCard
    {
        #region Required properties
        public override string DisplayName => "Star Power";

        public override string Description => "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 600.\nOn spend, strike any tile.\nthis card has +3 <b><color=#FFBF00>Repeat</color></b>.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Repeat }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 2;

        public override int RepeatAmount => 3;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Attack, ComponentTrait.Selection, ComponentTrait.Basic }.ToILCPP();

        // This card has several repeats as-is, so it doesn't need any more, block!
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Repeated }.ToILCPP();

        // Tactical and smooth are exceptionally strong on repeated cards, and chain is really good on cards where you can guarantee kills, so we block 'em!
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Chain, ComponentName.Tactical, ComponentName.Smooth }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new StrikeTileEffectTask(new TargetValue()));
            taskList.Add(new StarcycleTask(6, onSpendList).Convert());
            
            return taskList;
        }

        // The parameters the players selections need to satisfy to play this card, in this case, it just needs to be a tile
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add(new Selection(
                    new AndCondition(new IsTypeCondition<Coord>(new TargetValue()))));
            return selections;
        }
    }
}
