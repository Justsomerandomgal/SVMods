using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppLanguage.Lua;
using Il2CppStarVaders;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

namespace HistoryPack
{
    internal class FossilTask : AModTask
    {
        // This task strikes adjacent tiles and has +1 range for every 10 artifacts
        public FossilTask()
        {

        }

        public override IEnumerator Execute(ATask taskInstance)
        {
            // Don't want to clog the console log, so disabling logging in preview
            if (!taskInstance.IsPreviewModeView)
                Melon<Core>.Logger.Msg("Executing Shell Shock");
            int amount = Core.ArtifactCount(taskInstance.EncounterView.ArtifactViewDict);
            if (!taskInstance.IsPreviewModeView)
                Melon<Core>.Logger.Msg("Current artifact count: " + amount);
            if (amount > 5) 
            for (int i = 0; i < amount / 6; i++)
                yield return taskInstance.TaskEngine.ProcessTask(new CreateCardTask((int)ModContentManager.GetModCardName<Archeology>(), ComponentName.Sandy, rarity: new())).Cast<Il2CppSystem.Object>();
        }
    }
}
