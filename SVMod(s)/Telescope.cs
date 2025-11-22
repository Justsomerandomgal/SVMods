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
    internal class Telescope : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Telescope";

        public override string Description => "When your turn begins, <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr> 1500.\nOn spend, gain a <b><color=#FFBF00>Shooting Star</color></b> in your hand.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<CardName> MoreInfoCards => new HashSet<CardName>()
        {
            new ShootingStar().CardName
        }.ToILCPP();

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue())))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new StarcycleTask(15, new List<ATask>() { new CreateCardTask((int)ModContentManager.GetModCardName<ShootingStar>(), Pile: Pile.Hand, rarity: new()) }.ToILCPP()).Convert()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
