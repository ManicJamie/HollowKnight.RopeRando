using System.Collections.Generic;
using ItemChanger.UIDefs;
using ItemChanger;
using RandomizerMod.RC;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.Settings;
using MenuChanger;
using ItemChanger.Tags;
using UnityEngine.UIElements;
using System.Numerics;

namespace RopeRando.IC
{
    public static class ICManager
    {
        public static RopeLocation ropeLocation = new()
        {
            sceneName = SceneNames.Ruins2_03,
            name = Consts.LocationName
        };

        public static RopeItem ropeItem = new()
        {
            name = Consts.ItemName,
            UIDef = new MsgUIDef
            {
                name = new BoxedString("WK Chandelier Cut"),
                shopDesc = new BoxedString("Call in Del-Boy to do some maintenance on the Watcher Knights' chandelier."),
                sprite = new ItemChangerSprite("ShopIcons.Downslash") // TODO: change this
            }
        };

        /// <summary>
        /// Define the items & locations we are adding
        /// </summary>
        public static void DefineItemLoc()
        {
            Finder.DefineCustomItem(ropeItem);

            ropeLocation.AddTag(LocationMetadataTag());
            Finder.DefineCustomLocation(ropeLocation);
        }

        /// <summary>
        /// Creates the location tag used for RandoMapMod
        /// </summary>
        private static Tag LocationMetadataTag()
        {
            var t = new InteropTag
            {
                Message = "RandoSupplementalMetadata"
            };
            t.Properties["ModSource"] = nameof(RopeRando);
            t.Properties["MapLocations"] = new (string, float, float)[]
            {
                (SceneNames.Ruins2_03, -0.1f, 0.6f)
            };
            return t;
        }
    }
}
