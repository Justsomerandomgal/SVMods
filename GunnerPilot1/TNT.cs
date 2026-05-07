using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GunnerPilot1;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace GunnerPilot1
{
    internal class TNT : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "TNT";

        public override string Description => "The first time each turn when you <b><color=#FFBF00>Overheat</color></b>, detonate with +1 radius, also striking your mech.";

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Starter;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Overheat, MoreInfoWordName.Breakdown }.ToILCPP();

        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if said task is the OverheatTask, and the artifact has not been used yet
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerCondition = new()
            {
                new (Trigger.PostTask, new AndCondition(new IsTypeCondition<OverheatTask>(new RunningTaskValue()), new NotCondition(new IsArtifactUsedCondition(artifactID))))
            };
            
            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID, true),
                new StrikeTileEffectTask(new PlayerCoordValue()),
                new DetonationTask(new PlayerCoordValue(), radius: 1)
            };

            TriggerEffect onOverheat = new TriggerEffect(triggerCondition.ToILCPP(), triggerTasks.ToILCPP());

            // The trigger of an artifact, in this case: After every task, if said task is the OverheatTask, and the artifact has not been used yet
            triggerCondition = new()
            {
                new (Trigger.PreTask, new IsTypeCondition<StartTurnTask>(new RunningTaskValue()))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            triggerTasks = new List<ATask>()
            {
                new SetArtifactUsedTask(artifactID, false)
            };

            return new List<TriggerEffect>()
            {
                onOverheat, new TriggerEffect(triggerCondition.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
