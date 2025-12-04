global using Il2CppStarVaders;
global using Il2CppCollections = Il2CppSystem.Collections.Generic;
using Harmony;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

[assembly: MelonInfo(typeof(HistoryPack.Core), "HistoryPack", "1.0", "RandomGuy", null)]
[assembly: MelonGame("Pengonauts", "StarVaders")]

namespace HistoryPack
{
    public class Core : SVMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
        protected override void EarlyRegisterMod()
        {
            base.EarlyRegisterMod();
            RegisterMoreInfoPanel("Relic", "<b><color=#FFBF00>Relics</color></b> are temporary <b><color=#FFBF00>Artifacts</color></b></nobr> that you lose at the end of the encounter.\n\n<b><color=#FFBF00>Relics</color></b> that are created can not trigger immediately afterwards.");
            RegisterMoreInfoPanel("Tactical", "<font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Tactical</i></font></b></smallcaps></color></size></voffset> <b><color=#FFBF00>Component</color></b></nobr> is a card upgrade that draws 1 card when you play the card.");
            RegisterMoreInfoPanel("Artifact", "<b><color=#FFBF00>Artifacts</color></b></nobr> are rewards that permanently alter you run in some way.");
        }

        // This is a section that was used for testing, this is way faster then trying to find your card/artifact every single time
        /*protected override void LateRegisterMod()
        {
            RegisterContentMod(new PilotModification(PilotName.Roxy)
            {
                targetPilot = PilotName.Roxy,
                startingCards = new List<PlayerCardData>()
                {   new (CardName.Shift),
                    new (CardName.Shift),
                    new (CardName.Shift),
                    new (ModContentManager.GetModCardName<Archeology>()),
                    new (ModContentManager.GetModCardName<Revolution>()),
                    new (ModContentManager.GetModCardName<Obituary>()),
                    new (ModContentManager.GetModCardName<ShellShock>()),
                    new (ModContentManager.GetModCardName<SpiceTrade>())
                }.ToILCPP(),
                startingArtifacts = new List<ArtifactName>
                {   ModContentManager.GetModArtifactName<SteamEngine>(),
                    ModContentManager.GetModArtifactName<Fossil>(),
                    ModContentManager.GetModArtifactName<Reliquary>(),
                }.ToILCPP()
            });
        }*/

        public static int ArtifactCount(Il2CppSystem.Collections.Generic.Dictionary<ID, ArtifactView> views)
        {
            int count = views.Count;
            foreach (ArtifactView view in views.Values)
                if (view.ArtifactModel.Rarity == Rarity.Modifier)
                    count--;
            return count;
        }
    }
}