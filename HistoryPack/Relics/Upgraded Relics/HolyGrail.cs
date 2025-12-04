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

        public override string Description => "Draw +1 card on every turn not divisible by 3.";

        public override ClassName Class => ClassName.UniquePack;

        // This is technically a modifier, but because neither IsEncounterModifier or IsCurseModifier is set to true it won't show up as a modifier through the normal way
        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            TurnNumberValue t = new TurnNumberValue();
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask and the turn number is NOT a multiple of 3
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new NotCondition (new OrCondition (new EqualsCondition(t, 3), new EqualsCondition(t, 6), new EqualsCondition(t, 9), new EqualsCondition(t, 12), new EqualsCondition(t, 15), new EqualsCondition(t, 18), new EqualsCondition(t, 21), new EqualsCondition(t, 24), new EqualsCondition(t, 27), new EqualsCondition(t, 30)))
                ))
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
