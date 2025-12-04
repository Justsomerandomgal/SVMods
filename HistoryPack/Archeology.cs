using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace HistoryPack
{
    internal class Archeology : AModCard
    {
        #region Required properties
        public override string DisplayName => "Archeology!";

        public override string Description => "Gain a <b><color=#FFBF00>Relic</color></b> and strike your mech.\nPurge this card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Junk }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        public override Rarity Rarity => Rarity.Rare;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Purge, ModContentManager.GetModMoreInfoName("Relic") }.ToILCPP();

        public override bool RequiresPlayerEntity => false;

        public override int ClassBaseCost => 1;

        public override bool IsPurged => true;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed }.ToILCPP();

        // This card is busted with repeats, so we block!
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Repeated }.ToILCPP();

        // This card is purged so these components make total sense (I do not think mirror is smart though)
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.TacticalPlus, ComponentName.SwiftPlus }.ToILCPP();

        // These traits are just sad to get on this card
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Fluid, ComponentName.Tactical, ComponentName.Refreshed, ComponentName.Broken, ComponentName.Retained }.ToILCPP();

        // This makes the shadow around the card match the border (the junk shape)
        public override CardViewData CardViewData => GetStandardCardViewDataX("Archeology.png");

        protected CardViewData GetStandardCardViewDataX(string imageName, float pixelsPerUnit = 100,
            FilterMode filter = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp,
            bool localName = true, bool warnOnFail = true)
        {
            Sprite sprite = GetStandardSprite(imageName, pixelsPerUnit, filter, wrapMode, localName, warnOnFail);
            if (sprite == null)
                return null;
            CardViewData output = new CardViewData(CardName, sprite, null);
            output._outlineSprite = sprite;
            return output;
        }

        //public override bool HasSpecialCardViewPrefab => false;
        //public override bool HasSpecialCardModel => true;

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new GainRelicTask(), new ConditionalTask(new PlayerEntityExistsCondition(), new List<ATask>() { new StrikeTileEffectTask(new PlayerCoordValue()) }.ToILCPP()) }.ToILCPP();
        }
    }
}
