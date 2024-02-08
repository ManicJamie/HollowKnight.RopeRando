using System;
using RandoSettingsManager.SettingsManagement;
using RandoSettingsManager.SettingsManagement.Versioning;

namespace RopeRando
{
    internal class RandoSettingsManagerProxy : RandoSettingsProxy<ConnectionSettings, Signature>
    {
        internal Func<ConnectionSettings> getter;
        internal Action<ConnectionSettings> setter;

        public RandoSettingsManagerProxy(Func<ConnectionSettings> getter, Action<ConnectionSettings> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public override string ModKey => nameof(RopeRando);

        public override VersioningPolicy<Signature> VersioningPolicy => new StructuralVersioningPolicy(getter);

        public override bool TryProvideSettings(out ConnectionSettings? sent)
        {
            sent = getter();
            return sent.Enabled;
        }

        public override void ReceiveSettings(ConnectionSettings? received)
        {
            setter(received ?? new());
        }
    }
}