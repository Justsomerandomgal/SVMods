using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppStarVaders;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine.SocialPlatforms;
using static Il2CppSystem.Linq.Expressions.Interpreter.OrInstruction;

namespace HistoryPack
{
    internal class HistoryPack : AModPack
    {
        public override string DisplayName => "History";

        public override string Description => "Adds artifact and temporary artifact (Relic) synergy cards and artifacts to the card pool.";

        //The lists that define which cards and artifacts should go in the pack
        public override Il2CppCollections.HashSet<CardName> cards => new HashSet<CardName>()
        {
            ModContentManager.GetModCardName<Obituary>(),
            ModContentManager.GetModCardName<Revolution>(),
            ModContentManager.GetModCardName<Archeology>(),
            ModContentManager.GetModCardName<ShellShock>(),
            ModContentManager.GetModCardName<SpiceTrade>()
        }.ToILCPP();

        public override Il2CppCollections.HashSet<ArtifactName> artifacts => new HashSet<ArtifactName>()
        {
            ModContentManager.GetModArtifactName<SteamEngine>(),
            ModContentManager.GetModArtifactName<Fossil>(),
            ModContentManager.GetModArtifactName<Reliquary>()
        }.ToILCPP();
    }
}
