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
    internal class PhotonicGiftTask : AModTask
    {
        // This task gives you 2500 stars if you have less than 20000
        public PhotonicGiftTask()
        {

        }
        
        public override IEnumerator Execute(ATask taskInstance)
        {
            if (taskInstance.IsPreviewModeView)
                yield break;
                
            Melon<Core>.Logger.Msg("Beginning Photonic Gift task execution.");
            int currentStars = taskInstance.EncounterModel.Values[EncounterValue.StarBucks];
            Melon<Core>.Logger.Msg("You have " + currentStars + " stars");

            if (currentStars < 200)
            {
                Melon<Core>.Logger.Msg("Gaining stars");
                yield return taskInstance.TaskEngine.ProcessTask(new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 25, true)).Cast<Il2CppSystem.Object>();
            }
            else
                Melon<Core>.Logger.Msg("Already too much stars");
        }
    }
}