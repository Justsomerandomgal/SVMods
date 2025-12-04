using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class SpiceTrade : AModCard
    {
        #region Required properties
        public override string DisplayName => "Spice Trade";

        public override string Description => "Gain a <b><color=#FFBF00>Relic</color></b> and create a <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Tactical</i></font></b></smallcaps></color></size></voffset> copy of <font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Hallucination</i></font></b></smallcaps></color></size></voffset> in both your discard and draw pile.\nIncrease the cost of this card by 1 each time played.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() {  }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Legendary;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { ModContentManager.GetModMoreInfoName("Relic"), ModContentManager.GetModMoreInfoName("Tactical") }.ToILCPP();

        public override Il2CppCollections.HashSet<CardName> MoreInfoCards => new HashSet<CardName>() { CardName.Hallucination }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 1;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.SelectionLess, ComponentTrait.Basic }.ToILCPP();

        // This card is busted with repeats, so we block!
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Repeated }.ToILCPP();

        // Fluid would make this potentially expensive card free, which isn't good, and refreshed can unburn this card giving you infinite plays. Also, broken doesn't get its cost increased
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Fluid, ComponentName.Refreshed, ComponentName.Broken }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { 
                new GainRelicTask(),
                new CreateCardTask((int)CardName.Hallucination, ComponentName.Tactical, Pile: Pile.Discard, rarity: new()),
                new CreateCardTask((int)CardName.Hallucination, ComponentName.Tactical, Pile: Pile.Draw, rarity: new()),
            }.ToILCPP();
        }

        // This increase the price when played, but this isn't affected by repeats
        public override Il2CppCollections.List<TriggerEffect> GetTriggerEffects(OnCreateIDValue cardID)
        {
            // The trigger of a special effect of this card, in this case: After this card is played (basically)
            List<Il2CppSystem.ValueTuple<Trigger, ACondition>> triggerConditions = new()
            {
                new (Trigger.PostTask, new AndCondition(
                    new IsTypeCondition<PlayCardEndTask>(new RunningTaskValue()),
                    new EqualsCondition(new CurrentCardIDValue(), cardID)
                ))
            };

            // The tasks to perform if triggered
            List<ATask> triggerTasks = new()
            {
                new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(new TargetValue(), cardID), ArgKey.BaseEnergyCost, Operation.Add, 1))
            };

            return new List<TriggerEffect>()
            {
                new TriggerEffect(triggerConditions.ToILCPP(), triggerTasks.ToILCPP())
            }.ToILCPP();
        }
    }
}
