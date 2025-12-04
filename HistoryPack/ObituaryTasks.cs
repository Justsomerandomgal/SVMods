using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppStarVaders;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class ObituaryTask : AModTask
    {
        // This task gives a given card +1 repeat for every 7 artifacts you have
        public ObituaryTask()
        {

        }

        public ObituaryTask(Il2CppSystem.Object CardID)
        {
            SetArg(ArgKey.CardID, CardID);
        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // Disabling preview is technically not needed as you can't preview this task, but better be safe then sorry
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }
            Melon<Core>.Logger.Msg("Executing Obituary intitialization");
            Il2CppSystem.Object cardID = taskInstance.GetArg<Il2CppSystem.Object>(ArgKey.CardID);
            int amount = Core.ArtifactCount(taskInstance.EncounterView.ArtifactViewDict);
            Melon<Core>.Logger.Msg("Current artifact count: " + amount);
            for (int i = 0; i < amount / 7; i++)
            {
                Melon<Core>.Logger.Msg("Adding +1 Repeat");
                yield return taskInstance.TaskEngine.ProcessTask(new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(new TargetValue(), cardID), ArgKey.RepeatAmount, Operation.Add, 1))).Cast<Il2CppSystem.Object>();
            }
        }
    }
    internal class ObituaryTask2 : AModTask
    {
        // This task gives a given card +1 repeat if you have a multiple of 7 artifacts
        public ObituaryTask2()
        {

        }
        public ObituaryTask2(Il2CppSystem.Object CardID)
        {
            SetArg(ArgKey.CardID, CardID);
        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // Disabling preview is technically not needed as you can't preview this task, but better be safe then sorry
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }
            
            Melon<Core>.Logger.Msg("Executing Obituary update");
            Il2CppSystem.Object cardID = taskInstance.GetArg<Il2CppSystem.Object>(ArgKey.CardID);
            int amount = Core.ArtifactCount(taskInstance.EncounterView.ArtifactViewDict);
            Melon<Core>.Logger.Msg("Current artifact count: " + amount);
            if (amount % 7 == 0)
            {
                Melon<Core>.Logger.Msg("Adding +1 Repeat");
                yield return taskInstance.TaskEngine.ProcessTask(new AddPlayCardModTask(new PlayCardModifierModel(new EqualsCondition(new TargetValue(), cardID), ArgKey.RepeatAmount, Operation.Add, 1))).Cast<Il2CppSystem.Object>();
            }
        }
    }
}
