using MenuChanger.MenuElements;
using MenuChanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemChanger;

namespace RopeRando.Rando
{
    static class ConnectionMenuButton
    {
        public static SmallButton? button;

        public static void UpdateButtonColor()
        {
            if (button is null) { return; }
            button.Text.color = RopeRando.Instance.settings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
        }

        public static bool Build(MenuPage landingPage, out SmallButton settingsButton)
        {
            button = new SmallButton(landingPage, "Rope Rando");

            UpdateButtonColor();
            button.OnClick += () =>
            {
                RopeRando.Instance.settings.Enabled = !RopeRando.Instance.settings.Enabled;
                UpdateButtonColor();
            };
            settingsButton = button;
            return true;
        }
    }
}
