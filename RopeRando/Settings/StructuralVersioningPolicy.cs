using System.Collections.Generic;
using System;
using System.Linq;
using RandoSettingsManager.SettingsManagement.Versioning;

namespace RopeRando
{
    internal class StructuralVersioningPolicy : VersioningPolicy<Signature>
    {
        internal Func<ConnectionSettings> settingsGetter;

        public StructuralVersioningPolicy(Func<ConnectionSettings> getter)
        {
            settingsGetter = getter;
        }

        public override Signature Version => new() { FeatureSet = FeatureSetForSettings(settingsGetter()) };

        private static List<string> FeatureSetForSettings(ConnectionSettings rs) =>
            SupportedFeatures.Where(f => f.feature(rs)).Select(f => f.name).ToList();

        public override bool Allow(Signature s) => s.FeatureSet.All(name => SupportedFeatures.Any(sf => sf.name == name));

        private static List<(Predicate<ConnectionSettings> feature, string name)> SupportedFeatures = new()
        {
            (rs => rs.Enabled, "ItemAndLocation")
        };
    }

    internal struct Signature
    {
        public List<string> FeatureSet;
    }
}