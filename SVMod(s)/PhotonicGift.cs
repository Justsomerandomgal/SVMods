using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class PhotonicGift : AModCard
    {
        #region Required properties
        public override string DisplayName => "Photonic Gift";

        public override string Description => "Give a <nobr><sprite=\"TextIcons\" name=\"Shield\"><b><color=#FFBF00> Shield</color></b></nobr>-less invader a <nobr><sprite=\"TextIcons\" name=\"Shield\"><b><color=#FFBF00> Shield</color></b></nobr> and gain 3000 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr>.\nPurge this card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Purge, MoreInfoWordName.Shield, ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override bool IsPurged => true;

        public override int ClassBaseCost => 1;

        // This card is a bit more tricky with it's components, so we just use our own group of components that do work well with this card (including mirror (AKA replicating) because funnies :]  )
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.TacticalPlus, ComponentName.Replicating, ComponentName.Boosted, ComponentName.Risky }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            taskList.Add(new ShieldTileEffectTask(new TargetValue(), true));
            taskList.Add(new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 30, true));

            return taskList;
        }

        // The parameters the players selections need to satisfy to play this card, in this case: any tile, that is not empty, where the entity is an enemy, and that entity does not have an shield
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add( new Selection(
                    new AndCondition(new IsTypeCondition<Coord>(new TargetValue()),
                    new NotCondition(new IsCoordEmptyCondition(new TargetValue())),
                    new IsEnemyCondition(new CoordEntityValue(new TargetValue())),
                    new NotCondition(new IsEntityShieldedCondition(new CoordEntityValue(new TargetValue()))))));
            return selections;
        }
    }
}
