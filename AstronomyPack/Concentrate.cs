using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class Concentrate : AModCard
    {
        #region Required properties
        public override string DisplayName => "Concentrate";

        public override string Description => "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 700.\nOn spend, strike a tile up to 3 tiles away twice.\nOr draw 1 card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Attack, ComponentTrait.Selection, ComponentTrait.Basic, ComponentTrait.Repeated }.ToILCPP();

        // Wild would make this card garbage doodoo, so we block it
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new StrikeTileEffectTask(new TargetValue(), type: GridFX.MeteorFall));
            onSpendList.Add(new StrikeTileEffectTask(new TargetValue(), type: GridFX.FireballExplosion));
            taskList.Add(new ConditionalTask(new IsTypeCondition<ExtraSelectionModelID>(new TargetValue()), new List<ATask>{ new DrawTopDrawPileTask() }.ToILCPP(), new List<ATask>{ new StarcycleTask(7, onSpendList).Convert() }.ToILCPP()));

            return taskList;
        }
        
        // The parameters the players selections need to satisfy to play this card, in this case, any tile in range 3
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            return new List<Selection>()
            {
                new Selection(
                    new OrCondition(
                    new AndCondition(new IsTypeCondition<Coord>(new TargetValue()),
                        new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 0, 3)),
                    new ExtraSelectionCondition(new TargetValue())))
            }.ToILCPP();
        }
    }
}
