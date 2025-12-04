using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Grenade : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Hand Grenade";

        public override string Description => "When your turn ends, 50% chance to strike a random <nobr><sprite=\"TextIcons\" name=\"Shield\"><b><color=#FFBF00> Shield</color></b></nobr>-less invader.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Created;
        #endregion

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: Before every task, if the task is EndTurnTask
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new IsTypeCondition<EndTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash, ConditionalTask only performs StrikeRandomInvaderTask if the coin flip (affected by Lucky Dice) succeeds
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new ConditionalTask (new RandomChanceCondition(0.5f), new List<ATask>{ new StrikeRandomShieldlessInvaderTask() }.ToILCPP())
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
