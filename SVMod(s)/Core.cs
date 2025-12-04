
global using Il2CppStarVaders;
global using Il2CppCollections = Il2CppSystem.Collections.Generic;
using Harmony;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

[assembly: MelonInfo(typeof(ModdedPack1.Core), "AstronomyPack", "1.2", "RandomGuy", null)]
[assembly: MelonGame("Pengonauts", "StarVaders")]

namespace ModdedPack1
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
            RegisterMoreInfoPanel("Starcycle", "<nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Starcycle</color></b></nobr>: If you do not have the listed amount of <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr>, gain that amount.\n\nOtherwise, spend that amount of <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> to perform the \"On Spend\" ability on the card.");
            RegisterMoreInfoPanel("Starry", "<font=\"StarvadersGun-Regular SDF\"><size=150%><voffset=-0.11em>Starry</i></font></b></smallcaps></color></size></voffset> <b><color=#FFBF00>Component</color></b></nobr> is a card upgrade that gives 300 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr> when you play the card.");
        }

        // This is a section that was used for testing, this is way faster then trying to find your card/artifact every single time
        /*protected override void LateRegisterMod()
        {
            RegisterContentMod(new PilotModification(PilotName.Roxy)
            {
                targetPilot = PilotName.Roxy,
                startingCards = new List<PlayerCardData>()
                {   new (ModContentManager.GetModCardName<ShootingStar>()),
                    new (ModContentManager.GetModCardName<Consentrate>()),
                    new (ModContentManager.GetModCardName<Orbit>()),
                    new (ModContentManager.GetModCardName<PhotonicGift>()),
                    new (ModContentManager.GetModCardName<StarPower>()),
                    new (ModContentManager.GetModCardName<CosmicRays>())
                }.ToILCPP(),
                startingArtifacts = new List<ArtifactName>
                {   ModContentManager.GetModArtifactName<Telescope>(),
                    ModContentManager.GetModArtifactName<Stargaze>(),
                    ModContentManager.GetModArtifactName<Starmap>(),
                }.ToILCPP()
            });
        }*/
    }
}