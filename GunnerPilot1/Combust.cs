using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace GunnerPilot1
{
    internal class Combust : AModCard
    {
        #region Required properties
        public override string DisplayName => "Combust!";

        public override string Description => "Detonate any non-Boss entity.\nPurge this card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Rare;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Purge }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 2;

        public override bool IsPurged => true;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Repeated, ComponentTrait.Selection }.ToILCPP();

        // This card uses selection, so we block components that require more selections
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.SelectionLess }.ToILCPP();

        // Mirror and Tactical+ make sens for this expensive self-purge card
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.Replicating, ComponentName.TacticalPlus }.ToILCPP();

        // These traits don't really work on this card - or are just sad to get
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Chain, ComponentName.Echo, ComponentName.Refreshed, ComponentName.Tactical }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new SacrificePuppetTask(new CoordEntityValue(new TargetValue())), new DetonationTask(new TargetValue()) }.ToILCPP();
        }
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            return new List<Selection>()
            {
                new Selection(new AndCondition(
                    new IsTypeCondition<Coord>(new TargetValue()),
                    new NotCondition(new IsCoordEmptyCondition(new TargetValue())),
                    new NotCondition(new EqualsCondition(new PlayerCoordValue(), new TargetValue())),
                    new NotCondition(new IsBossCondition(new CoordEntityValue(new TargetValue())))),
                    selectionDescriptor: SelectionDescriptor.CardToBurn)
            }.ToILCPP();
        }
    }
}
