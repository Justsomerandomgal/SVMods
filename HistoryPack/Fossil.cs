using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Fossil : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Fossil";

        public override string Description => "When you gain a <b><color=#FFBF00>Relic</color></b>, strike a random <nobr><sprite=\"TextIcons\" name=\"Shield\"><b><color=#FFBF00> Shield</color></b></nobr>-less invader.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Relic"), MoreInfoWordName.Shield }.ToILCPP();

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if said task is the GainRelicTask (this looks a bit different because GainRelicTask is a modded task)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerCondition = new()
            {
                new (Trigger.PostTask, new IsCustomTaskOfIDCondition(new RunningTaskValue(), ModContentManager.GetModTaskID<GainRelicTask>()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new StrikeRandomShieldlessInvaderTask()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerCondition.ToILCPP(), triggerTasks.ToILCPP()),
            }.ToILCPP();
        }
    }
}
