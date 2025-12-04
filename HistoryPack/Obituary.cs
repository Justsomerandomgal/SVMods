using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppStarVaders;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class Obituary : AModCard
    {
        #region Required properties
        public override string DisplayName => "Obituary";

        public override string Description => "Strike a random invader, this card has +1 <b><color=#FFBF00>Repeat</color></b> for every 7 <b><color=#FFBF00>Artifacts</color></b></nobr> you have.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Common;
        #endregion

        // Hidden Traits define some things that other cards and artifacts can work around, for example, random is relevant for lucky dice and road trip
        public override Il2CppCollections.HashSet<CardTrait> HiddenTraits => new HashSet<CardTrait>() { CardTrait.Random }.ToILCPP();

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Repeat, ModContentManager.GetModMoreInfoName("Artifact") }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Repeated, ComponentTrait.SelectionLess, ComponentTrait.Basic, ComponentTrait.Attack }.ToILCPP();

        // Tactical and smooth are exceptionally strong on repeated cards, and chain is really good on cards kills are random, so we block 'em! (Also, nuh uh wild on selectionless)
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Chain, ComponentName.Tactical, ComponentName.Smooth, ComponentName.Arcane, ComponentName.Flux, ComponentName.Wild }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new StrikeRandomInvaderTask() }.ToILCPP();
        }

        //These tasks are performed when this card is created
        public override Il2CppCollections.List<ATask> GetOnCreateTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new ObituaryTask(cardID).Convert() }.ToILCPP();
        }

        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue cardID)
        {
            // The triggers of a special effect of this card. In this case, we have 2 triggers and sets of tasks, one is meant for initialisation and the other meant for updating 
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new IsCustomTaskOfIDCondition(new RunningTaskValue(), ModContentManager.GetModTaskID<GainRelicTask>())),
            };

            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> initConditions = new()
            {
                new(Trigger.PreTask, new AndCondition(
                    new IsTypeCondition<StartTurnTask>(new RunningTaskValue()),
                    new EqualsCondition(new TurnNumberValue(), 0)
                ))
            };

            // The tasks to perform if triggered
            List <ATask> initTasks = new List<ATask>()
            {
                //new ObituaryTask(cardID).Convert()
            };

            List<ATask> triggerTasks = new List<ATask>()
            {
                new ObituaryTask2(cardID).Convert()
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(initConditions.ToILCPP(), initTasks.ToILCPP()),
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
