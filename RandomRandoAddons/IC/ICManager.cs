using System.Collections.Generic;
using ItemChanger.UIDefs;
using ItemChanger;
using RandomizerMod.RC;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.Settings;
using MenuChanger;

namespace RopeRando.IC
{
    public static class ICManager
    {
        public static RopeLocation ropeLocation = new()
        {
            objectName = "chandelier",
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

        public static void DefineItemLoc()
        {
            Finder.DefineCustomItem(ropeItem);
            Finder.DefineCustomLocation(ropeLocation);
        }
    }
}
