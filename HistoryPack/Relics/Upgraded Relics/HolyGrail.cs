using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class HolyGrail : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Holy Grail";

        public override string Description => "Draw +1 card each turn.";

        public override ClassName Class => ClassName.UniquePack;

        // Rare artifacts don't exist and thus never show up
        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            TurnNumberValue t = new TurnNumberValue();
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask and the turn number is NOT a multiple of 3
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new IsTypeCondition<StartTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new DrawTopDrawPileTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())                
            }.ToILCPP();
        }
    }
}
