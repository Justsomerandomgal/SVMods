using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Goblet : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Goblet";

        public override string Description => "1 in 3 chance to draw +1 card every turn.";

        public override ClassName Class => ClassName.UniquePack;

        // Created artifacts don't exist and thus never show up
        public override Rarity Rarity => Rarity.Created;
        #endregion

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask and the turn number is even
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new IsTypeCondition<StartTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new ConditionalTask(new RandomChanceCondition(0.3333333f), new List<ATask>{ new DrawTopDrawPileTask() }.ToILCPP())
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())               
            }.ToILCPP();
        }
    }
}
