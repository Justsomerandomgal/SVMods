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
    internal class Stargaze : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Stargaze";

        public override string Description => "Whenever you move your mech or when your turn begins, gain 100 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr>.";

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        public override ContextPreviewType ContextPreviewType => ContextPreviewType.MoveCards;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask OR After every task, if the task is MoveEntityTask, and the entity to move is equal to the player entity
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<MoveEntityTask>(new RunningTaskValue()),
                    new EqualsCondition (new CoordEntityValue(new PlayerCoordValue()), new TaskArgValue(new RunningTaskValue<MoveEntityTask>(), ArgKey.EntityID)))),
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue())))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 1, true)
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
