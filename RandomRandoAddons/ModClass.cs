using Modding;

using RopeRando.IC;

namespace RopeRando
{
    public class RopeRando : Mod
    {
        new public string GetName() => "Rope Randomizer";
        public override string GetVersion() => "v1.0";
        public override void Initialize()
        {
            Log("Initializing...");

            ICManager manager = new();

            manager.RegisterItemsAndLocations();
            manager.Hook();

            Log("Initialized.");
        }
    }
}