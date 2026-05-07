using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVModHelper;
using SVModHelper.ModContent;
using UnityEngine;

namespace GunnerPilot1
{
    internal class JabFire : AModCard
    {
        #region Required properties
        public override string DisplayName => "Jab Fire!";

        public override string Description => "Strike an adjacent tile and fire 1 <b><color=#FFBF00>Bullet</color></b> upwards.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack }.ToILCPP();

        // This makes this card count as a card with Fire! in it's name, so that it applies the effects of for example flash fire
        public override Il2CppCollections.HashSet<CardTrait> HiddenTraits => new HashSet<CardTrait>() { CardTrait.Fire }.ToILCPP();

        public override ClassName Class => ClassName.Gunner;

        public override Rarity Rarity => Rarity.Starter;
        #endregion

        public override PilotName PilotUnique => PilotName.Roxy;

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Bullet }.ToILCPP();

        public override bool RequiresPlayerEntity => true;

        public override int ClassBaseCost => 1;

        public override bool IsPurged => false;

        // Some fitting developer made traits are added to this card so that components that fit that trait can get attached
        public override Il2CppCollections.HashSet<ComponentTrait> AllowedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.Costed, ComponentTrait.Attack, ComponentTrait.Repeated, ComponentTrait.Selection, ComponentTrait.NonPurge, ComponentTrait.Bullet }.ToILCPP();

        // This card uses selection, so we block components that require more selections
        public override Il2CppCollections.HashSet<ComponentTrait> BlockedComponentTraits => new HashSet<ComponentTrait>() { ComponentTrait.SelectionLess }.ToILCPP();

        // These traits don't really work on this card
        public override Il2CppCollections.HashSet<ComponentName> BlockedComponentNames => new HashSet<ComponentName>() { ComponentName.Wild, ComponentName.Fiery }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPreSelectionTaskList(OnCreateIDValue cardID)
        {
            return new List<ATask> { new StrikeTileEffectTask(new TargetValue()), new FireBulletTask(new PlayerCoordValue(), animationSourceCoord: new()) }.ToILCPP();
        }
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            return new List<Selection>()
            {
                new Selection(
                    new AndCondition(new IsTypeCondition<Coord>(new TargetValue()),
                    new DistanceCondition(new PlayerCoordValue(), new TargetValue(), 1, 1)),
                    selectionDescriptor: SelectionDescriptor.Tile)
            }.ToILCPP();
        }
    }
}
