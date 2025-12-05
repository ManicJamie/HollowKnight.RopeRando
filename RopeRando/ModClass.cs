using Modding;
using RandomizerMod.Menu;
using RandomizerMod.Logging;
using RandomizerMod.RC;
using RopeRando.IC;
using RopeRando.Rando;
using RandoSettingsManager.SettingsManagement;

namespace RopeRando
{
    public class RopeRando : Mod, IGlobalSettings<ConnectionSettings>
    {
        private static RopeRando? _instance;
        public static RopeRando Instance { get => _instance!; }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();
        public override void Initialize()
        {
            Log("Initializing...");

            _instance = this;

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
            RandoSettingsManager.RandoSettingsManagerMod.Instance.RegisterConnection(new SimpleSettingsProxy<ConnectionSettings>(
                this,
                (st) =>
                {
                    if (st is null)
                    {
                        settings.Enabled = false;
                    } else
                    {
                        settings = st;
                    }
                },
                () => { return settings.Enabled ? settings : null; }
                ));
        }

        public ConnectionSettings settings = new();

        public void OnLoadGlobal(ConnectionSettings s) => settings = s;

        public ConnectionSettings OnSaveGlobal() => settings;
    }
}