using SVModHelper;
using SVModHelper.ModContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModdedPack1
{
    internal class StarryComp : AModComponent
    {
        public override string DisplayName => "Starry";

        public override string Description => "When played, gain 300 <nobr><sprite=\"TextIcons\" name=\"Stars\"><b><color=#FFBF00> Stars</color></b></nobr>";

        public override ClassName Class => ClassName.Neutral;

        // As of SVModHelper version 0.2.0, there is an issue involving displaying components, so we need to override the normal system for the sprite and use our own (which is a direct copy paste with one different variable), feel free to copy for your own modded components!
        public override Sprite Sprite => GetStandardSpriteX("StarryComp.png");

        protected Texture2D GetTextureX(string imageName, FilterMode filter = FilterMode.Bilinear, bool localName = true, bool warnOnFail = true)
        {
            if (!TryGetContentData(imageName, out byte[] data, localName, warnOnFail))
                return null;
            Texture2D texture = new Texture2D(2, 2) { filterMode = filter, wrapMode = TextureWrapMode.Clamp };
            texture.LoadImage(data);
            return texture;
        }

        protected Sprite GetStandardSpriteX(string imageName, float pixelsPerUnit = 100, FilterMode filter = FilterMode.Bilinear, bool localName = true, bool warnOnFail = true)
        {
            Texture2D texture = GetTextureX(imageName, filter, localName, warnOnFail);
            if (texture == null)
                return null;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit/*, 0, SpriteMeshType.Tight*/);
        }
        
        // The task of components is added on top of the existing tasks of the card
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            taskList.Add(new EncounterValueOperationTask(EncounterValue.StarBucks, Operation.Add, 3, true));
            return taskList;
        }
    }
}