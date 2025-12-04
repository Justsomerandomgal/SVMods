using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class HolyGrenade : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Holy Hand Grenade";

        public override string Description => "When your turn ends, strike a random <nobr><sprite=\"TextIcons\" name=\"Shield\"><b><color=#FFBF00> Shield</color></b></nobr>-less invader.\n";

        public override ClassName Class => ClassName.UniquePack;

        // This is technically a modifier, but because neither IsEncounterModifier or IsCurseModifier is set to true it won't show up as a modifier through the normal way
        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is EndTurnTask
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new IsTypeCondition<EndTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new StrikeRandomShieldlessInvaderTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
