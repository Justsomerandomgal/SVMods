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
    internal class VentHeat : AModCard
    {
        #region Required properties
        public override string DisplayName => "Vent Heat";

        public override string Description => "Lose 1 <b><color=#FFBF00>Heat</color></b> and strike the tile directly below you.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Created;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Heat }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 0;

        public override bool IsPurged => false;

        // This card can get these components from the EmergencyVent artifact, so we define these so you can see the possible components in the compendium
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.Tactical, ComponentName.Swift, ComponentName.Refreshed }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new StrikeTileEffectTask(new SumValueCoord(Coord.Unit(Direction.S), new PlayerCoordValue())), new EncounterValueOperationTask(EncounterValue.Heat, Operation.Subtract, 1) }.ToILCPP();
        }
    }
}
