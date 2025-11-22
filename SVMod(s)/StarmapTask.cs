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
    internal class StarmapTask : AModTask
    {
        // This task adds the starry component to a random componentless card for every 5 card you have in your deck
        public StarmapTask()
        {
            
        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // Disabling preview is technically not needed as you can't preview this artifact, but better be safe then sorry
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }
            Melon<Core>.Logger.Msg("Beginning Starmap execution.");
            // Get a list of all cards, and a list of all cards without components
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Draw).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID())).ToList();
            List<CardModel> cardsWithoutComponents = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Draw).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID()))
                .Where(card => !card.HasComponent).ToList();

            Melon<Core>.Logger.Msg("Found " + cards.Count + " cards.");
            
            int amountToAdd = cards.Count / 5;
            Melon<Core>.Logger.Msg("Adding components to " + amountToAdd + " eligible cards.");
            Random r = new();
            if (amountToAdd > 0)
                for (int i = 0; i < amountToAdd && cardsWithoutComponents.Count > 0; i++)
                {
                    int target = r.Next(cardsWithoutComponents.Count);
                    CardModel card = cardsWithoutComponents[target];

                    Melon<Core>.Logger.Msg("Adding component...");
                    yield return taskInstance.TaskEngine.ProcessTask(new AddComponentTask(card.CardID.BoxIl2CppObject(), new StarryComp().ComponentName)).Cast<Il2CppSystem.Object>();
                    cardsWithoutComponents.RemoveAt(target);
                }
        }
    }
}
