using MenuChanger.MenuElements;
using MenuChanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RopeRando.Rando
{
    static class ConnectionMenuButton
    {
        public static bool Build(MenuPage landingPage, out SmallButton settingsButton)
        {
            var button = new SmallButton(landingPage, "Rope Rando");
            var settings = RopeRando.Instance.settings;

            void UpdateButtonColor()
            {
                button.Text.color = settings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            }

            UpdateButtonColor();
            button.OnClick += () =>
            {
                settings.Enabled = !settings.Enabled;
                UpdateButtonColor();
            };
            settingsButton = button;
            return true;
        }
    }
}
