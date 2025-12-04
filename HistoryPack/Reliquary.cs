using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Reliquary : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Reliquary";

        public override string Description => "When the encounter begins, gain a <b><color=#FFBF00>Relic</color></b>.\n<b><color=#FFBF00>Relics</color></b> have a 2 in 3 chance to be upgraded to be more powerful, this chance can not be modified.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Relic") }.ToILCPP();

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask and the artifact is not used
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition( 
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new NotCondition(new IsArtifactUsedCondition(artifactID))
                ))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash and display "used" on top of it
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID, true),
                new GainRelicTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
