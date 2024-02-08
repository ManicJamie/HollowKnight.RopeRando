using ItemChanger;
using UnityEngine;

namespace RopeRando.IC
{
    public class RopeItem : AbstractItem
    {
        public override void GiveImmediate(GiveInfo info)
        {
            PlayerData.instance.SetBool("watcherChandelier", true);

            // If we're already in the same scene as the chandelier, drop it.
            if (GameManager.instance.sceneName == SceneNames.Ruins2_03)
            {
                GameObject.Find("chandelier").LocateMyFSM("Control").SetState("Drop");
            }
        }
    }
}
