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

        public override string Description =>  "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 300.\nOn spend, move 1 to 3 tiles.\nthis card has +2 <b><color=#FFBF00>Repeat</color></b>.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Move }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Repeat, ModContentManager.GetModMoreInfoName("Starcycle") }.ToILCPP();

        public override int RepeatAmount => 2;

        public override bool RequiresPlayerEntity => true;

        // This card is pretty tricky to get components for, as it has base repeats (which makes draws really strong) and it's free (which makes discount components useless)
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.Retained, ComponentName.Flux, ComponentName.Refreshed }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            Il2CppCollections.List<ATask> onSpendList = new();
            onSpendList.Add(new MoveEntityTask(new CoordEntityValue(new PlayerCoordValue()), new TargetValue()));
            taskList.Add(new StarcycleTask(3, onSpendList).Convert());

            return taskList;
        }

        // The parameters the players selections need to satisfy to play this card, in this case: Any tile, where an entity can move to, that is range 1-3 from the player 
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add(new Selection(new AndCondition(
                new IsTypeCondition<Coord>(new TargetValue()),
                new IsMoveableCoordCondition(new TargetValue()),
                new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 3)),
                selectionDescriptor: SelectionDescriptor.Tile));

            return selections;
        }
    }
}
