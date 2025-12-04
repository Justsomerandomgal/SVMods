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


namespace HistoryPack
{
    internal class GainRelicTask : AModTask
    {
        // This task gives a random RELIC (relics are special artifacts that are destroyed at the end of the encounter)
        public GainRelicTask()
        {
            Melon<Core>.Logger.Msg("Steam Machine Task initialised.");
        }
        public override IEnumerator Execute(ATask taskInstance)
        {
            // Disabling preview is technically not needed as you can't preview this artifact, but better be safe then sorry
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }
            Melon<Core>.Logger.Msg("Beginning Steam Machine execution.");
            yield return taskInstance.TaskEngine.ProcessTask(new CreateArtifactTask(ModContentManager.GetModArtifactName<Grail>())).Cast<Il2CppSystem.Object>();
        }
    }
}
