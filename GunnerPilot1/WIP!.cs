using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVModHelper;
using SVModHelper.ModContent;
using System.Threading.Tasks;
using Il2CppStarVaders;
using UnityEngine;

namespace GunnerPilot1
{
    public class LuffyPilot : AModPilot
    {

        public override string DisplayName => "WIP!";
        public override string Description => "- WIP\n- Also WIP";
        public override int Complexity => 2;

        public override ClassName ClassName => ClassName.Gunner;

        public override string BaseImagePath => "Luffy";
        public override Sprite FrontPortrait => GetStandardSprite("LuffySelect2.png");

        public override Il2CppCollections.List<PlayerCardData> StartingCards =>
            new List<PlayerCardData>{
                new(CardName.Shift),
                new(CardName.Shift),
                new(CardName.Shift),
                new(CardName.Shift),
                new(CardName.Fire),
                new(CardName.Fire),
                new(CardName.Fire),
                new(CardName.Fire),
                //new(ModContentManager.GetModCardName<JabFire>()),
                //new(ModContentManager.GetModCardName<JabFire>()),
                //new(ModContentManager.GetModCardName<JabFire>()),
                //new(ModContentManager.GetModCardName<JabFire>()),
                //new(ModContentManager.GetModCardName<Retreat>()),
                new(CardName.Salve),
            }.ToILCPP();

        public override Il2CppCollections.List<ArtifactName> StartingArtifacts =>
            new List<ArtifactName>()
            {
                ModContentManager.GetModArtifactName<TNT>()
            }.ToILCPP();
    }
}
