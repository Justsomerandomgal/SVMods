using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModdedPack1;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace ModdedPack1
{
    internal class Starmap : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Starmap";

        public override string Description => "When the encounter begins, give <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Starry</i></font></b></smallcaps></color></size></voffset> to a random card for every 5 cards in your deck.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Component, ModContentManager.GetModMoreInfoName("Starry") }.ToILCPP();

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: Before every task, if the task is StartTurnTask and the artifact is not used
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PreTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new NotCondition (new IsArtifactUsedCondition(artifactID))))
                    
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash and display "used" on top of it
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID, true),
                new StarmapTask().Convert()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
