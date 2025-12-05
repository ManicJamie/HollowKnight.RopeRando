using ItemChanger.UIDefs;
using ItemChanger;
using ItemChanger.Tags;

namespace RopeRando.IC
{
    public static class ICManager
    {
        internal static ChandelierSprite chandelierSprite = new();

        private static readonly RopeLocation ropeLocation = new()
        {
            sceneName = SceneNames.Ruins2_03,
            name = Consts.LocationName
        };

        private static RopeItem RopeItem()
        {
            return new()
            {
                name = Consts.ItemName,
                UIDef = new MsgUIDef
                {
                    name = new BoxedString("WK Chandelier Cut"),
                    shopDesc = new BoxedString("Call in Del-Boy to do some maintenance on the Watcher Knights' chandelier."),
                    sprite = chandelierSprite
                }
            };
        }

        /// <summary>
        /// Define the items & locations we are adding
        /// </summary>
        public static void DefineItemLoc()
        {
            Finder.DefineCustomItem(RopeItem());

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
            // t.Properties["LocationPinSprite"] = chandelierSprite; // Doesn't appear to work (and if it did, this isn't a suitable pin sprite regardless)
            return t;
        }
    }
}
