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
        // This task gives you a relic if you have none or if it's the first turn
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
            int amount = Core.RelicCount(taskInstance.EncounterView.ArtifactViewDict);
            Melon<Core>.Logger.Msg("Current relic count: " + amount);
            if (amount == 0)
                yield return taskInstance.TaskEngine.ProcessTask(new GainRelicTask().Convert()).Cast<Il2CppSystem.Object>();
            else
                yield return taskInstance.TaskEngine.ProcessTask(new ConditionalTask(new EqualsCondition(1, new TurnNumberValue()), new List<ATask>{ new GainRelicTask().Convert() }.ToILCPP())).Cast<Il2CppSystem.Object>();
        }
    }
}
