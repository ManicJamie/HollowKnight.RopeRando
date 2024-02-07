using System.Linq;
using ItemChanger.Locations;
using ItemChanger;
using Satchel;
using ItemChanger.Util;

namespace RopeRando.IC
{
    public class RopeLocation : AutoLocation
    {
        public string objectName;

        protected override void OnLoad()
        {
            Events.AddFsmEdit(sceneName, new(objectName, "Control"), ModifyRopeBehaviour);
        }

        protected override void OnUnload()
        {
            Events.RemoveFsmEdit(sceneName, new(objectName, "Control"), ModifyRopeBehaviour);
        }

        private void ModifyRopeBehaviour(PlayMakerFSM fsm)
        {
            fsm.AddState("GiveItem");
            fsm.AddCustomAction("GiveItem", () => {
                ItemUtility.GiveSequentially(Placement.Items, Placement, new GiveInfo()
                {
                    FlingType = FlingType.Everywhere,
                    MessageType = MessageType.Corner,
                });
            });

            // Get the original sounds and cut animation from the real Drop state
            var sound = fsm.GetAction("Drop", 1);
            var anim = fsm.GetAction("Drop", 3);

            fsm.AddAction("GiveItem", sound);
            fsm.AddAction("GiveItem", anim);

            fsm.ChangeTransition("Hit", "DESTROY", "GiveItem");

            // If we already obtained the item at this location, set the rope to a broken state already:
            if (Placement.Items.All(x => x.IsObtained()))
            {
                fsm.AddAction("Init", anim);
                fsm.RemoveTransition("Init", "FINISHED");
            }
            else
            {
                // Stop it from removing the rope

                fsm.RemoveAction("Activate", 2); // Stop it deactivating the rope, even if the data is there.
                fsm.AddTransition("Activate", "FINISHED", "Idle"); // Go back to hittable state, even if we already dropped the chandelier.
            }
        }
    }
}
