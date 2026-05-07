using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    // This task adds the starry component to a random componentless card in your hand
    internal class ShootingStarTask : AModTask
    {
        public ShootingStarTask()
        {

        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // If this task is in preview mode (thus your only hovering your selection and haven't made it yet), we do not show what the outcome is
            if (taskInstance.IsPreviewModeView)
            {
                yield return taskInstance.TaskEngine.ProcessTask(new StrikeTileEffectTask(new TargetValue())).Cast<Il2CppSystem.Object>();
                yield break;
            }

            Melon<Core>.Logger.Msg("Beginning Shooting Star execution.");
            yield return taskInstance.TaskEngine.ProcessTask(new ConditionalTask(new OrCondition(new IsEntityShieldedCondition(new CoordEntityValue(new TargetValue())), new NotCondition(new EqualsCondition(new EntityHealthValue(new CoordEntityValue(new TargetValue())), 1))), 
                new List<ATask> { new StrikeTileEffectTask(new TargetValue(), type: GridFX.LandingDust), new PushTileEffectTask(new TargetValue(), UnityEngine.Random.RandomRangeInt(0, 4), 1) }.ToILCPP(),
                new List<ATask> { new StrikeTileEffectTask(new TargetValue(), Direction: (Direction)UnityEngine.Random.RandomRangeInt(0, 4), Distance: 1, type: GridFX.LandingDust) }.ToILCPP())).Cast<Il2CppSystem.Object>();
            // Makes a list of cards where the card doesn't have a component and costs at least 1 in one of the categories, to rig the chances in the player's favour
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent && (card.HeatCost > 0 || card.PowerCost > 0 || card.ManaCost > 0)).ToList();

            Melon<Core>.Logger.Msg("Found " + cards.Count + " prefered cards.");

            // If no prefered cards are found, instead adds the component to eligible cards
            if (cards.Count == 0)
            {
                // Makes a list of cards where the card doesn't have a component (and isn't a token and purged, I added this because the card could add the component to itself)
                cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent && !(card.IsPurged && card.IsToken)).ToList();
                Melon<Core>.Logger.Msg("Found " + cards.Count + " Eligible cards.");
            }

            // If there is an eligble card, adds the component to it
            if (cards.Count > 0)
            {
                CardModel card = cards[UnityEngine.Random.RandomRangeInt(0, cards.Count)];
                Melon<Core>.Logger.Msg("Adding component...");
                yield return taskInstance.TaskEngine.ProcessTask(new AddComponentTask(card.CardID.BoxIl2CppObject(), ModContentManager.GetModComponentName<StarryComp>())).Cast<Il2CppSystem.Object>();
            }
        }
    }
}
