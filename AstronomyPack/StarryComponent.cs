using SVModHelper;
using SVModHelper.ModContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModdedPack1
{
    internal class StarryComp : AModComponent
    {
        public override string DisplayName => "Starry";

        public override string Description => "Reduce cost by 1, removes itself when played. When you win the encounter, gain 500 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr>";

        public override ClassName Class => ClassName.Neutral;

        // The triggereffects of components is added on top of the existing triggers of the card
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue cardID)
        {
            // The trigger of a special effect of this card, in this case: When you win the encounter
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new IsTypeCondition<WinEncounterTask>(new RunningTaskValue()))
            };
            // The tasks to perform if triggered
            List<ATask> triggerTasks = new()
            {
                new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 5, true)
            };
            TriggerEffect EndEncounterTrigger = new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP());

            // A second triggerCondition for a second effect, this one activates after its card has been played
            triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<PlayCardEndTask>(new RunningTaskValue()),
                    new EqualsCondition(new CurrentCardIDValue(), cardID)
                ))
            };
            // Replaces itself with an empty component slot
            triggerTasks = new()
            {
                new AddComponentTask(cardID, ComponentName.None)
            };
            return new List<TriggerEffect>()
            {
                EndEncounterTrigger,
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }

        // These effects occur when this component is added to a card, this discounts the card by 1 (only for the first use) as this component gets removed after said use
        public override Il2CppCollections.List<ATask> GetOnCreateTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(cardID, new TargetValue()), ArgKey.BaseEnergyCost, Operation.Subtract, 1, true)) }.ToILCPP();
        }
    }
}