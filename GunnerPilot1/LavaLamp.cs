using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Il2CppNewtonsoft.Json.Linq;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace GunnerPilot1
{
    internal class LavaLamp : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Lava lamp";

        public override string Description => "When you burn a card, there is a 50% chance to make an unburnt copy of that card in your draw pile.";

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Burnt }.ToILCPP();

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if said task is the GainRelicTask (this looks a bit different because GainRelicTask is a modded task)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new AndCondition (new IsTypeCondition<SetCardBurntTask>(new RunningTaskValue()), new EqualsCondition(new TaskArgValue(new RunningTaskValue(), ArgKey.Value, true), true)))
            };
            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ConditionalTask(new RandomChanceCondition(0f), new List<ATask> { new ProcessArtifactTask(artifactID), new DuplicateCardTask(new TaskArgValue(new RunningTaskValue(), ArgKey.CardID), Pile.Draw) }.ToILCPP(), isSkipPreview: true)
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP()),
            }.ToILCPP();
        }
    }
}
