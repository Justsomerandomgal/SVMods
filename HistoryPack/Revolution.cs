using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Revolution : AModCard
    {
        #region Required properties
        public override string DisplayName => "Revolution";

        public override string Description => "Move 1 to 4 tiles.\nIf you have 10 or more <b><color=#FFBF00>Artifacts</color></b></nobr>, reduce the cost of the next card you play by 1.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Move }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Artifact") }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Repeated, ComponentTrait.Move, ComponentTrait.Basic }.ToILCPP();

        // Wild is uhhh... not good here
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new MoveEntityTask(new CoordEntityValue(new PlayerCoordValue()), new TargetValue()) }.ToILCPP();
        }

        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new RevolutionTask() }.ToILCPP();
        }

        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<Selection> selections = new();

            selections.Add(new Selection(new AndCondition(
                new IsTypeCondition<Coord>(new TargetValue()),
                new IsMoveableCoordCondition(new TargetValue()),
                new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 4)),
                selectionDescriptor: SelectionDescriptor.Tile));

            return selections;
        }
    }
}
