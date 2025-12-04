using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class PhilStone : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Philosopher's Stone";

        public override string Description => "On every odd turn, 50% chance of making the most expensive card in your hand free this turn.\nOtherwise, reduce the cost of a random card in hand by 1.";

        public override ClassName Class => ClassName.UniquePack;

        // This is technically a modifier, but because neither IsEncounterModifier or IsCurseModifier is set to true it won't show up as a modifier through the normal way
        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Free }.ToILCPP();

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if the task is StartTurnTask and the turn number is even
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new NotCondition(new IsEvenCondition (new TurnNumberValue()))
                ))
            };

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash, ConditionalTask only performs QuickFireTask if the coin flip (affected by Lucky Dice) succeeds, otherwise it does AntiMagTask
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID),
                new ConditionalTask(new RandomChanceCondition(0.5f), new List<ATask>() { new QuickFireTask() }.ToILCPP(), new List<ATask>() { new AntiMagTask() }.ToILCPP(), true)
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
