using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace GunnerPilot1
{
    internal class EmergencyVent : AModArtifact
    {
        #region Required properties
        public override string DisplayName => "Emergency Vent";

        public override string Description => "The first time each turn when you <b><color=#FFBF00>Overheat</color></b>, create a <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Vent Heat</i></font></b></smallcaps></color></size></voffset> in your draw pile and give it a random <b><color=#FFBF00>Component</color></b>.";

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Overheat, MoreInfoWordName.Component }.ToILCPP();

        public override Il2CppCollections.HashSet<CardName> MoreInfoCards => new HashSet<CardName>() { ModContentManager.GetModCardName<VentHeat>() }.ToILCPP();

        // This allows common artifacts to be in that one special encounter
        public override bool CanBeDuplicated => true;

        // This function defines when and what this artifact does
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue artifactID)
        {
            // The trigger of an artifact, in this case: After every task, if said task is the GainRelicTask (this looks a bit different because GainRelicTask is a modded task)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerCondition = new()
            {
                new (Trigger.PostTask, new AndCondition (new IsTypeCondition<OverheatTask>(new RunningTaskValue()), new NotCondition(new IsArtifactUsedCondition(artifactID))))
            };

            Random rand = new Random();
            List<ComponentName> ComponentSet = new List<ComponentName>() { ComponentName.Tactical, ComponentName.Swift, ComponentName.Refreshed };
            ComponentName Component = ComponentSet[rand.Next(3)];

            // The tasks to perform if triggered, ProcessArtifactTask makes the image of the artifact flash
            List<ATask> triggerTasks = new List<ATask>()
            {
                new ProcessArtifactTask(artifactID, true),
                new CreateCardTask((int)ModContentManager.GetModCardName<VentHeat>(), Component, rarity: new ())
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
