using Modding;
using RandomizerMod.Menu;
using MenuChanger;
using MenuChanger.MenuElements;
using RandomizerMod.Logging;
using RandomizerMod.RandomizerData;
using System.IO;
using RandomizerMod.RC;
using RopeRando.IC;
using RopeRando.Rando;

namespace RopeRando
{
    public class RopeRando : Mod, IGlobalSettings<ConnectionSettings>
    {
        private static RopeRando _instance;
        public static RopeRando Instance { get { _instance ??= new(); return _instance; } }

        static RopeRando() { _instance ??= new(); }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();
        public override void Initialize()
        {
            Log("Initializing...");

            // Define items for IC
            ICManager.DefineItemLoc();

            // Define items
            RCData.RuntimeLogicOverride.Subscribe(100f, RandoManager.RegisterItemAndLocationLogic);
            // Add items to current generation
            RequestBuilder.OnUpdate.Subscribe(100f, RandoManager.BuildRequest);
            // Log settings
            SettingsLog.AfterLogSettings += RandoManager.LogRandoSettings;

            RandomizerMenuAPI.AddMenuPage(_ => { }, ConnectionMenuButton.Build);

            if (ModHooks.GetMod("RandoSettingsManager") != null)
            {
                HookRSM();
            }
            
            Log("Initialized.");
        }

        private void HookRSM()
        {
            RandoSettingsManager.RandoSettingsManagerMod.Instance.RegisterConnection(
                new RandoSettingsManagerProxy(() => settings, rs => settings = rs));
        }

        public ConnectionSettings settings = new();

        public void OnLoadGlobal(ConnectionSettings s)
        {
            settings = s;
        }

        public ConnectionSettings OnSaveGlobal() => settings;
    }
}