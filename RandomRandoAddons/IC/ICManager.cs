using System.Collections.Generic;
using ItemChanger.UIDefs;
using ItemChanger;
using RandomizerMod.RC;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.Settings;

namespace RopeRando.IC
{
    internal class ICManager
    {
        public void RegisterItemsAndLocations()
        {
            RopeLocation ropeLocation = new()
            {
                objectName = "chandelier",
                sceneName = SceneNames.Ruins2_03,
                name = "chandelierLocation",
                nonreplaceable = true
            };

            RopeItem ropeItem = new()
            {
                name = "chandelierItem",
                UIDef = new MsgUIDef
                {
                    name = new BoxedString("WK Chandelier Cut"),
                    shopDesc = new BoxedString("Call in Del-Boy to do some maintenance on the Watcher Knights' chandelier."),
                    sprite = new ItemChangerSprite("ShopIcons.Downslash") // TODO: change this
                }
            };

            Finder.DefineCustomLocation(ropeLocation);
            Finder.DefineCustomItem(ropeItem);
        }

        public void Hook()
        {
            RCData.RuntimeLogicOverride.Subscribe(0, ApplyLogic);
            RequestBuilder.OnUpdate.Subscribe(0.3f, AddRope);

            On.UIManager.StartNewGame += UIManager_StartNewGame;
        }

        private void AddRope(RequestBuilder rb)
        {
            rb.AddItemByName("chandelierItem");
            rb.AddLocationByName("chandelierLocation");
        }

        private void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            lmb.AddItem(new EmptyItem("chandelierItem"));
            lmb.AddLogicDef(new("chandelierLocation", "Ruins2_03[bot1] + CLAW"));
        }

        private void UIManager_StartNewGame(On.UIManager.orig_StartNewGame orig, UIManager self, bool permaDeath, bool bossRush)
        {
            orig(self, permaDeath, bossRush);

            ItemChangerMod.CreateSettingsProfile(false);

            AbstractPlacement placement = Finder.GetLocation("chandelierLocation").Wrap();
            ItemChangerMod.AddPlacements(new List<AbstractPlacement>() { placement });
        }
    }
}
