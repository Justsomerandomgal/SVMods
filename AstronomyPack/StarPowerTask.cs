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
    internal class StarPowerTask : AModTask
    {
        // This task draws a card and makes it free and gives it +1 repeat until played
        public StarPowerTask()
        {

        }
        
        public override IEnumerator Execute(ATask taskInstance)
        {
            yield return taskInstance.TaskEngine.ProcessTask(new DrawTopDrawPileTask()).Cast<Il2CppSystem.Object>();

            if (taskInstance.IsPreviewModeView)
                yield break;
                
            Melon<Core>.Logger.Msg("Beginning Star Power task execution.");
            List<CardModel> cards = taskInstance.EncounterModel.CardPlayModel.GetPile(Pile.Hand).ToMono()
                .Select(cardID => taskInstance.EncounterModel.GetModelItem<CardModel>(cardID.ToID())).ToList();

            Melon<Core>.Logger.Msg("Giving modifiers to " + cards[0].CardName);
            yield return taskInstance.TaskEngine.ProcessTask(new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(new TargetValue(), cards[0].ID.Cast<Il2CppSystem.Object>()), ArgKey.RepeatAmount, Operation.Add, 1, true, true))).Cast<Il2CppSystem.Object>();
            yield return taskInstance.TaskEngine.ProcessTask(new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(new TargetValue(), cards[0].ID.Cast<Il2CppSystem.Object>()), ArgKey.BaseEnergyCost, Operation.Replace, 0, true, true))).Cast<Il2CppSystem.Object>();

        }
    }
}