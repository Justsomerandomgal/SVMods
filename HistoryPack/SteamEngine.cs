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
    internal class SteamEngine : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Steam Engine";

        public override string Description => "At the end of turns 1 and 3, gain a <b><color=#FFBF00>Relic</color></b>.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Relic") }.ToILCPP();

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is EndTurnTask and it's turn 1 or turn 3
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new AndCondition(
                    new IsTypeCondition<EndTurnTask>(new RunningTaskValue()),
                    new OrCondition (new EqualsCondition(new TurnNumberValue(), 1), new EqualsCondition(new TurnNumberValue(), 3))
                ))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash and if the turn number is 3 (as per the conditional task), it will display "used" on top of it
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ConditionalTask(new EqualsCondition(new TurnNumberValue(), 3), new List<ATask>(){ new ProcessArtifactTask(artifactID, true) }.ToILCPP(), new List<ATask>{ new ProcessArtifactTask(artifactID) }.ToILCPP()),
                new GainRelicTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
