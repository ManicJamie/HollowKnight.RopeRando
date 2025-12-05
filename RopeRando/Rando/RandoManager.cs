using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.Logging;
using RandomizerMod.RandomizerData;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using System.IO;

namespace RopeRando.Rando
{
    public static class RandoManager
    {

        public static void RegisterItemAndLocationLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!RopeRando.Instance.settings.Enabled) { return; }

            lmb.AddItem(new EmptyItem(Consts.ItemName));

            lmb.AddLogicDef(new(Consts.LocationName, "Ruins2_03[bot1] + CLAW"));
        }

        public static void BuildRequest(RequestBuilder rb)
        {
            if (!RopeRando.Instance.settings.Enabled) { return; }

            rb.AddItemByName(Consts.ItemName);
            rb.AddLocationByName(Consts.LocationName);
        }

        public static void LogRandoSettings(LogArguments args, TextWriter w)
        {
            w.WriteLine("Logging RopeRando settings:");
            w.WriteLine(JsonUtil.Serialize(RopeRando.Instance.settings));
        }
    }
}
