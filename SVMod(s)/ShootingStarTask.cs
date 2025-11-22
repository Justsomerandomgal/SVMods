using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;
using Il2Cpp;
using MelonLoader;
using Il2CppInterop.Runtime.Injection;

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
            Melon<Core>.Logger.Msg("Beginning Shooting Star execution.");

            // Makes a list of cards where the card doesn't have a component (and isn't a token and purged, I added this because the card could add the component to itself)
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent && !(card.IsToken && card.IsPurged)).ToList();

            Melon<Core>.Logger.Msg("Found " + cards.Count + " eligible cards.");

            // If this task is in preview mode (thus your only hovering your selection and haven't made it yet), we do not show what the outcome is
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }

            // If there is an eligble card, adds the component to it
            if (cards.Count > 0)
            {
                Random r = new();
                CardModel card = cards[r.Next(cards.Count)];
                Melon<Core>.Logger.Msg("Adding component...");
                yield return taskInstance.TaskEngine.ProcessTask(new AddComponentTask(card.CardID.BoxIl2CppObject(), ModContentManager.GetModComponentName<StarryComp>())).Cast<Il2CppSystem.Object>();
            }
        }
    }
}
