using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppPixelCrushers.DialogueSystem;
using Il2CppStarVaders;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class SteamMachine : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Steam Machine";

        public override string Description => "When the encounter begins, and at the start of turn 3, gain a random <b><color=#FFBF00>Relic</color></b> this encounter";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        public override bool IsCurseModifier => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: Before every task, if the task is StartTurnTask and the artifact is not used
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new OrCondition (new EqualsCondition(new TurnNumberValue(), 1), new EqualsCondition(new TurnNumberValue(), 3))))

            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash and display "used" on top of it
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new ConditionalTask(new EqualsCondition(new TurnNumberValue(), 3), new List<ATask>(){ new ProcessArtifactTask(artifactID, true) }.ToILCPP()),
                new GainRelicTask().Convert()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
