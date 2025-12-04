using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Grail : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Grail";

        public override string Description => "On every even turn, draw a card. Lose this modifier at the end of the encounter";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Modifier;
        #endregion

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: Before every task, if the task is StartTurnTask and the artifact is not used
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new IsEvenCondition (new TurnNumberValue())))

            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash and display "used" on top of it
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new DrawTopDrawPileTask(),
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
                
            }.ToILCPP();
        }
    }
}
