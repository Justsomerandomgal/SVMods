using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppStarVaders;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace GunnerPilot1
{
    internal class Heatshield : AModCard
    {
        #region Required properties
        public override string DisplayName => "Heatshield";

        public override string Description => "Gain a <b><color=#FFBF00>Shield</color></b> and draw 1 card.\nBurn this card.\nIf you end your turn with this card in your hand, this card unburns and becoms Frozen."; // Add color & font for Frozen

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Rare;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Shield, MoreInfoWordName.Burnt, MoreInfoWordName.Frozen }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 0;

        public override bool IsPurged => false;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.SelectionLess, ComponentTrait.NonPurge }.ToILCPP();

        // This card uses selection, so we block components that require more selections
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Repeated, ComponentTrait.Selection }.ToILCPP();

        // These traits don't really work on this card
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild, ComponentName.Chain, ComponentName.Chilled, ComponentName.Echo, ComponentName.Broken }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new ShieldTileEffectTask(new PlayerCoordValue(), true), new DrawTopDrawPileTask(), new SetCardBurntTask(new CurrentCardIDValue()) }.ToILCPP();
        }

        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue cardID)
        {
            // The trigger of an artifact, in this case: After every task, if said task is the GainRelicTask (this looks a bit different because GainRelicTask is a modded task)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new AndCondition (new IsTypeCondition<EndTurnTask>(new RunningTaskValue()), new CardInHandButNotBeingPlayedCondition(cardID)))
            };
            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new SetCardBurntTask(cardID, false, true),
                new SetCardFrozenTask(cardID, true, true)
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP()),
            }.ToILCPP();
        }
    }
}
