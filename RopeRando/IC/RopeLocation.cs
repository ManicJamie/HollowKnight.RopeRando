using System.Linq;
using ItemChanger.Locations;
using ItemChanger;
using Satchel;
using ItemChanger.Util;
using ItemChanger.Tags;
using System.Numerics;

namespace RopeRando.IC
{
    public class RopeLocation : AutoLocation
    {
        // Since we're modifying a specific FSM on a specific scene, it makes sense to define those here
        const string chand_sceneName = SceneNames.Ruins2_03;
        public static readonly FsmID fsmID = new("chandelier", "Control");

        public RopeLocation()
        {
            sceneName = SceneNames.Ruins2_03;
        }

        protected override void OnLoad()
        {
            Events.AddFsmEdit(chand_sceneName, fsmID, ModifyRopeBehaviour);
        }

        protected override void OnUnload()
        {
            Events.RemoveFsmEdit(chand_sceneName, fsmID, ModifyRopeBehaviour);
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
