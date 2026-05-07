using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppStarVaders;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class Orbit : AModCard
    {
        #region Required properties
        public override string DisplayName => "Orbit";

        public override string Description => "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 500.\nOn spend, move 1 to 3 tiles.\nThis card has +1 range for every 3000 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> you have, up to +5.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Move }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Repeat, ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();


        public override bool RequiresPlayerEntity => true;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Basic, ComponentTrait.Move, ComponentTrait.Repeated, ComponentTrait.Selection, ComponentTrait.CardManip }.ToILCPP();

        // This card uses selection, so we block components that require more selections
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed }.ToILCPP();

        // Mirror and Tactical+ make sens for this expensive self-purge card
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.Refreshed, ComponentName.Retained, ComponentName.Tactical, ComponentName.Smooth, ComponentName.Jab }.ToILCPP();

        // These traits don't really work on this card - or are just sad to get
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Chain, ComponentName.Wild }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new MoveEntityTask(new CoordEntityValue(new PlayerCoordValue()), new TargetValue()));
            taskList.Add(new StarcycleTask(5, onSpendList).Convert());
            

            return taskList;
        }

        // The parameters the players selections need to satisfy to play this card, in this case: Any tile, where an entity can move to, that is range 1-3 from the player 
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add(new Selection(new AndCondition(
                new IsTypeCondition<Coord>(new TargetValue()),
                new IsMoveableCoordCondition(new TargetValue()),
                new OrCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 3),
                new AndCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 4), new IntGreaterThanCondition(new EncounterValueValue(EncounterValue.StarBucks), 29)),
                new AndCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 5), new IntGreaterThanCondition(new EncounterValueValue(EncounterValue.StarBucks), 59)),
                new AndCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 6), new IntGreaterThanCondition(new EncounterValueValue(EncounterValue.StarBucks), 89)),
                new AndCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 7), new IntGreaterThanCondition(new EncounterValueValue(EncounterValue.StarBucks), 119)),
                new AndCondition(new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 8), new IntGreaterThanCondition(new EncounterValueValue(EncounterValue.StarBucks), 149)))),
                selectionDescriptor: SelectionDescriptor.Tile));

            return selections;
        }
    }
}
