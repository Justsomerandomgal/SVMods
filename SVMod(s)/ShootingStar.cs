using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppStarVaders;
using SVModHelper;
using SVModHelper.ModContent;

namespace ModdedPack1
{
    public class ShootingStar : AModCard
    {
        #region Required properties
        public override string DisplayName => "Shooting Star";

        public override string Description => "Strike any tile twice.\nUpgrade a random card in your hand with <b><color=#FFBF00>Starry</color></b>.\nPurge this card.";

        public override Il2CppCollections.HashSet<CardTrait> Traits => new HashSet<CardTrait>() { CardTrait.Attack, CardTrait.Tactic }.ToILCPP();

        public override ClassName Class => ClassName.UniquePack;

        // "Created" makes this card unobtainable via rewards and the workshop
        public override Rarity Rarity => Rarity.Created;
        #endregion

        // MoreInfo___ are the extra info panels that show up when you right click an object, use them if you want to make your modded items easier to comprehend for newer players, but usually not needed (as new players wouldn't start modded)
        public override Il2CppCollections.HashSet<MoreInfoWordName> MoreInfoWords => new HashSet<MoreInfoWordName>() { MoreInfoWordName.Purge }.ToILCPP();

        public override bool IsPurged => true;

        public override bool IsToken => true;

        public override bool RequiresPlayerEntity => false;

        // Because this card can only be made by one legendary artifact, we don't need many components, just one for augment and blueprint is enough
        public override Il2CppCollections.HashSet<ComponentName> AllowedComponentNames => new HashSet<ComponentName>() { ComponentName.Tactical }.ToILCPP();

        // List of tasks to perform when played
        public override Il2CppCollections.List<ATask> GetPostSelectionTaskList(OnCreateIDValue cardID)
        {
            Il2CppCollections.List<ATask> taskList = new();
            taskList.Add(new StrikeTileEffectTask(new TargetValue()));
            taskList.Add(new StrikeTileEffectTask(new TargetValue()));
            taskList.Add(new ShootingStarTask().Convert());

            return taskList;
        }

        // The parameters the players selections need to satisfy to play this card, in this case, it just needs to be a tile
        public override Il2CppCollections.List<Selection> GetSelections(OnCreateIDValue cardID)
        {
            return new List<Selection>()
            {
                new Selection(
                    new AndCondition(new IsTypeCondition<Coord>(new TargetValue())))
            }.ToILCPP();
        }
    }
}
