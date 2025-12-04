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

        public override string Description => "Gain 300 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> when played";

        public override ClassName Class => ClassName.Neutral;

        // The triggereffects of components is added on top of the existing triggers of the card
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue cardID)
        {
            // The trigger of a special effect of this card, in this case: After this card is played (basically)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<PlayCardEndTask>(new RunningTaskValue()),
                    new EqualsCondition(new CurrentCardIDValue(), cardID)
                ))
            };

            // The tasks to perform if triggered
            List<ATask> triggerTasks = new()
            {
                new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 3, true),
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}