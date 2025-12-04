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
    internal class RevolutionTask : AModTask
    {
        // This task discounts the next card you play by 1 if you have at least 10 artifacts
        public RevolutionTask()
        {

        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // Doesn't do anything in preview mode so blocking the previews
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }

            Melon<Core>.Logger.Msg("Executing Revolution task");
            int amount = Core.ArtifactCount(taskInstance.EncounterView.ArtifactViewDict);
            Melon<Core>.Logger.Msg("Current artifact count: " + amount);
            if (amount >= 10)
                yield return taskInstance.TaskEngine.ProcessTask(new AddPlayCardModTask(new PlayCardModifierModel(new AlwaysTrueCondition(), ArgKey.BaseEnergyCost, Operation.Subtract, 1, true, true))).Cast<Il2CppSystem.Object>();
        }
    }
}
