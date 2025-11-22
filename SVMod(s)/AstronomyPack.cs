using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using ModdedPack1;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    internal class AstronomyPack : AModPack
    {
        public override string DisplayName => "Astronomy";

        public override string Description => "Adds <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Star</color></b></nobr> synergy cards and artifacts to the card pool.";

        //The lists that define which cards and artifacts should go in the pack
        public override Il2CppCollections.HashSet<CardName> cards => new HashSet<CardName>()
        {
            ModContentManager.GetModCardName<Orbit>(),
            ModContentManager.GetModCardName<Consentrate>(),
            ModContentManager.GetModCardName<PhotonicGift>(),
            ModContentManager.GetModCardName<StarPower>(),
            ModContentManager.GetModCardName<CosmicRays>()
        }.ToILCPP();

        public override Il2CppCollections.HashSet<ArtifactName> artifacts => new HashSet<ArtifactName>()
        {
            ModContentManager.GetModArtifactName<Starmap>(),
            ModContentManager.GetModArtifactName<Stargaze>(),
            ModContentManager.GetModArtifactName<Telescope>()
        }.ToILCPP();
    }
}
