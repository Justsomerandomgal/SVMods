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
using UnityEngine.UI;


namespace HistoryPack
{
    internal class GainRelicTask : AModTask
    {
        // This task gives a random RELIC (relics are artifacts that are removed at the end of the encounter)
        public GainRelicTask()
        {

        }
        
        public override IEnumerator Execute(ATask taskInstance)
        {
            // Disabling preview because some funtions here work weird with it
            if (taskInstance.IsPreviewModeView)
            {
                yield break;
            }

            Melon<Core>.Logger.Msg("Beginning Gain Relic execution.");
            Melon<Core>.Logger.Msg("Current artifact count (including modifiers): " + taskInstance.EncounterView.ArtifactViewDict.Count);
            if (taskInstance.EncounterView.ArtifactViewDict.Count == 9)
            {
                GridLayoutGroup replacementVals = taskInstance.EncounterView._artifactView14Parent;
                Il2CppSystem.Collections.Generic.Dictionary<ID, ArtifactView> a = taskInstance.EncounterView.ArtifactViewDict;
                taskInstance.EncounterView.ArtifactViewDict = new();
                taskInstance.EncounterView._currentArtifactParent.cellSize = new(-500000, -500000);
                taskInstance.EncounterView._currentArtifactParent = replacementVals;
                foreach (ArtifactView x in a.Values)
                    taskInstance.EncounterView.View_CreateArtifactView(x.ArtifactModel);
            }
            if (taskInstance.EncounterView.ArtifactViewDict.Count == 13)
            {
                GridLayoutGroup replacementVals = taskInstance.EncounterView._artifactView27Parent;
                Il2CppSystem.Collections.Generic.Dictionary<ID, ArtifactView> a = taskInstance.EncounterView.ArtifactViewDict;
                taskInstance.EncounterView.ArtifactViewDict = new();
                taskInstance.EncounterView._currentArtifactParent.cellSize = new(-500000, -500000);
                taskInstance.EncounterView._currentArtifactParent = replacementVals;
                foreach (ArtifactView x in a.Values)
                    taskInstance.EncounterView.View_CreateArtifactView(x.ArtifactModel);
            }
            
            int r = UnityEngine.Random.RandomRangeInt(0, 4);
            bool Upgrade = (UnityEngine.Random.RandomRangeInt(0,3) != 0) && taskInstance.EncounterModel.ArtifactIDs.ContainsKey(ModContentManager.GetModArtifactName<Reliquary>());
            if (Upgrade)
            {
                ArtifactName[] UpgradedRelics = { ModContentManager.GetModArtifactName<HolyGrail>(), ModContentManager.GetModArtifactName<PhilStone>(), ModContentManager.GetModArtifactName<HolyGrenade>(), ModContentManager.GetModArtifactName<AncientCoin>() };
                Melon<Core>.Logger.Msg("Gaining upgraded relic: " + UpgradedRelics[r]);
                // Create Artifact task does not give you a permanent artifact, it will be removed after the encounter!
                yield return taskInstance.TaskEngine.ProcessTask(new CreateArtifactTask(UpgradedRelics[r])).Cast<Il2CppSystem.Object>();
            }
            else
            {
                ArtifactName[] UnupgradedRelics = { ModContentManager.GetModArtifactName<Goblet>(), ModContentManager.GetModArtifactName<Stone>(), ModContentManager.GetModArtifactName<Grenade>(), ModContentManager.GetModArtifactName<Garment>() };
                Melon<Core>.Logger.Msg("Gaining normal relic: " + UnupgradedRelics[r]);
                yield return taskInstance.TaskEngine.ProcessTask(new CreateArtifactTask(UnupgradedRelics[r])).Cast<Il2CppSystem.Object>();
            }
        }
    }
}
