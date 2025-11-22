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
    internal class StarcycleTask : AModTask
    {
        // This task takes an amount of stars and a list of tasks, if you have more stars then the amount specified, it subtracts the stars and performs that list of tasks, otherwise it adds the amount of stars
        public StarcycleTask()
        {

        }
        public StarcycleTask(int starAmount, Il2CppCollections.List<ATask> onSpendTasks)
        {
            // Ints do not work well as an Arg (it crashes), so we put it in a list (Also, do note that stars internally are divided by 100 from what you see in game, so 1 star here is 100 stars in game)
            Il2CppCollections.List<int> x = new();
            x.Add(starAmount);
            SetArg(ArgKey.Misc, x);
            SetArg(ArgKey.TaskList, onSpendTasks);
        }
        
        public override IEnumerator Execute(ATask taskInstance)
        {
            // Get the relevant variables
            int starAmount = taskInstance.GetArg<Il2CppCollections.List<int>>(ArgKey.Misc)[0];
            Il2CppCollections.List<ATask> onSpendTasks = taskInstance.GetArg<Il2CppCollections.List<ATask>>(ArgKey.TaskList);
            int currentStars = taskInstance.EncounterView.currentTrueStarbucks;

            // If this task is in preview mode (thus your only hovering your selection and haven't made it yet), we do not subtract the stars and only show what else happens
            if (taskInstance.IsPreviewModeView)
            {
                if (currentStars >= starAmount)
                    foreach (ATask task in onSpendTasks)
                    {
                        yield return taskInstance.TaskEngine.ProcessTask(task).Cast<Il2CppSystem.Object>();
                    }
                yield break;
            }

            Melon<Core>.Logger.Msg("Beginning Starcycle execution.");
            
            if (currentStars < starAmount)
            {
                Melon<Core>.Logger.Msg("Too little stars to spend, gaining stars");
                yield return taskInstance.TaskEngine.ProcessTask(new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, starAmount, true)).Cast<Il2CppSystem.Object>();
            }
            else
            {
                Melon<Core>.Logger.Msg("Enough stars to spend, spending stars");
                // EncounterValue does not show changes in values during the encounter (though it does update on encounter end), so we change currentTrueStarbucks accordingly, we also use SetStarBucksText for instant star removal
                yield return taskInstance.TaskEngine.ProcessTask(new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, starAmount * -1)).Cast<Il2CppSystem.Object>();
                taskInstance.EncounterView.currentTrueStarbucks -= starAmount;
                taskInstance.EncounterView.SetStarBucksText(currentStars - starAmount);
                foreach (ATask task in onSpendTasks)
                {
                    yield return taskInstance.TaskEngine.ProcessTask(task).Cast<Il2CppSystem.Object>();
                }
            }
        }
    }
}