global using Il2CppStarVaders;
global using Il2CppCollections = Il2CppSystem.Collections.Generic;
using Harmony;
using MelonLoader;
using SVModHelper;
using SVModHelper.ModContent;

[assembly: MelonInfo(typeof(GunnerPilot1.Core), "GunnerPilot1", "1.0", "RandomGuy", null)]
[assembly: MelonGame("Pengonauts", "StarVaders")]

namespace GunnerPilot1
{
    public class Core : SVMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }

        protected override void LateRegisterMod()
        {
            /*RegisterContentMod(new PilotModification(PilotName.Roxy)
            {
                targetPilot = PilotName.Roxy,
                startingCards = new List<PlayerCardData>()
                {   
                    new(CardName.Shift),
                    new(CardName.Shift),
                    new(CardName.Shift),
                    new(CardName.Shift),
                    new(ModContentManager.GetModCardName<JabFire>()),
                    new(ModContentManager.GetModCardName<JabFire>()),
                    new(ModContentManager.GetModCardName<JabFire>()),
                    new(ModContentManager.GetModCardName<JabFire>()),
                    new(ModContentManager.GetModCardName<Retreat>()),
                    //new(ModContentManager.GetModCardName<Dynamite>()),
                    //new(ModContentManager.GetModCardName<GrapplingHook>()),
                    //new(ModContentManager.GetModCardName<Combust>()),
                    new(ModContentManager.GetModCardName<Heatshield>()),
                    new(CardName.Salve, ComponentName.Echo),
                }.ToILCPP(),
                startingArtifacts = new List<ArtifactName>
                {   
                    ModContentManager.GetModArtifactName<TNT>(),
                    ModContentManager.GetModArtifactName<LavaLamp>(),
                    ModContentManager.GetModArtifactName<EmergencyVent>(),
                }.ToILCPP()
            });*/
        }
    }
}