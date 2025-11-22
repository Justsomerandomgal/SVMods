
global using Il2CppStarVaders;
global using Il2CppCollections = Il2CppSystem.Collections.Generic;
using Harmony;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

[assembly: MelonInfo(typeof(ModdedPack1.Core), "Astronomy Pack", "1.0", "RandomGuy", null)]
[assembly: MelonGame("Pengonauts", "StarVaders")]

namespace ModdedPack1
{
    public class Core : SVMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
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