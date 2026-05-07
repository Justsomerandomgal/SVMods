using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace GunnerPilot1
{
    internal class Retreat : AModCard
    {
        #region Required properties
        public override string DisplayName => "Retreat";

        public override string Description => "Spawn an <b><color=#FFBF00>Auto-Bomb</color></b> on an adjacent tile, then move 1 to 3 tiles away.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Move, CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Starter;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Bomb }.ToILCPP();

        public override Il2CppCollections.HashSet<ItemName> MoreInfoItems => new HashSet<ItemName>() { ItemName.AdamBomb }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 1;

        public override bool IsPurged => false;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Repeated, ComponentTrait.Selection, ComponentTrait.Basic, ComponentTrait.Move, ComponentTrait.NonPurge }.ToILCPP();

        // This card uses selection, so we block components that require more selections
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.SelectionLess }.ToILCPP();

        // These traits don't really work on this card
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild, ComponentName.Chain }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<SelectionTaskGroup> GetSelectionTaskGroups(OnCreateIDValue cardID)
        {
            SelectionTaskGroup bomb = new SelectionTaskGroup(
                postSelectionTaskList: new List<ATask> { new CreateItemTask((int)ItemName.AdamBomb, new TargetValue()) }.ToILCPP(), 
                selections: new List<Selection> { new Selection(new AndCondition(
                    new IsTypeCondition<Coord>(new TargetValue()),
                    new IsMoveableCoordCondition(new TargetValue()),
                    new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 1)),
                    selectionDescriptor: SelectionDescriptor.Tile) }.ToILCPP());
            SelectionTaskGroup move = new SelectionTaskGroup(
                postSelectionTaskList: new List<ATask> { new MoveEntityTask(new CoordEntityValue(new PlayerCoordValue()), new TargetValue()) }.ToILCPP(),
                selections: new List<Selection> { new Selection(new AndCondition(
                    new IsTypeCondition<Coord>(new TargetValue()),
                    new IsMoveableCoordCondition(new TargetValue()),
                    new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 3)),
                    selectionDescriptor: SelectionDescriptor.Tile) }.ToILCPP());
            return new List<SelectionTaskGroup> { bomb, move }.ToILCPP();
        }
    }
}
