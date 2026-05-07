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

namespace HistoryPack
{
    // This task adds the starry component to a random componentless card in your hand
    internal class AncientCoinTask : AModTask
    {
        public AncientCoinTask()
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

            Melon<Core>.Logger.Msg("Beginning Ancient Coin Task execution.");
            // Makes a list of cards where the card doesn't have a component
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent).ToList();
            
            Melon<Core>.Logger.Msg("Found " + cards.Count + " eligible cards.");
            // If there is an eligble card, retain it
            if (cards.Count > 0)
            {
                CardModel card = cards[UnityEngine.Random.RandomRangeInt(0, cards.Count)];
                Melon<Core>.Logger.Msg("Retaining card...");
                yield return taskInstance.TaskEngine.ProcessTask(new SetCardRetainedTask(card.CardID.BoxIl2CppObject())).Cast<Il2CppSystem.Object>();
            }
        }
    }
    internal class AncientCoinTask2 : AModTask
    {
        public AncientCoinTask2()
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

            Melon<Core>.Logger.Msg("Beginning Ancient Coin Task 2 execution.");
            // Makes a list of cards where the card doesn't have a component
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent && !card.IsFrozen).ToList();

            Melon<Core>.Logger.Msg("Found " + cards.Count + " eligible cards.");
            // If there is an eligble card, retain it
            if (cards.Count > 0)
            {
                CardModel card = cards[UnityEngine.Random.RandomRangeInt(0, cards.Count)];
                Melon<Core>.Logger.Msg("Retaining card...");
                yield return taskInstance.TaskEngine.ProcessTask(new SetCardRetainedTask(card.CardID.BoxIl2CppObject(), false)).Cast<Il2CppSystem.Object>();
            }
        }
    }
}
