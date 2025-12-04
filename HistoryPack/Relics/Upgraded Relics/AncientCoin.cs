using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class AncientCoin : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Ancient Coin";

        public override string Description => "When your turn ends, upgrade a random card in hand with a random <b><color=#FFBF00>Component</color></b>.";

        public override ClassName Class => ClassName.UniquePack;

        // This is technically a modifier, but because neither IsEncounterModifier or IsCurseModifier is set to true it won't show up as a modifier through the normal way
        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Component }.ToILCPP();

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: Before every task, if the task is EndTurnTask
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new IsTypeCondition<EndTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new UpgradeRandomCardInHandTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
