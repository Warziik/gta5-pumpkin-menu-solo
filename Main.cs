using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.Native;
using NativeUI;

namespace pumpkin_menu
{
    public class Main : Script
    {
        MenuPool menuPool;
        UIMenu pumpkinMenu;

        public Main()
        {
            Tick += Main_Tick;
            KeyUp += Main_KeyUp;
            menuPool = new MenuPool();
            Setup();
        }

        #region Event Handlers
        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                pumpkinMenu.Visible = !pumpkinMenu.Visible;
            }
        }

        private void Main_Tick(object sender, System.EventArgs e)
        {
            if (menuPool != null)
            {
                menuPool.ProcessMenus();
            }
        }
        #endregion

        #region Menu
        private void Setup()
        {
            pumpkinMenu = new UIMenu("PumpkinMenu", "A basic menu.");
            menuPool.Add(pumpkinMenu);

            //UIResRectangle banner = new UIResRectangle();
            //banner.Color = Color.FromArgb(224, 123, 57, 255);
            //Sprite banner = new Sprite("shopui_title_barber", "shopui_title_barber", new Point(0, 0), new Size(0, 0));
            //pumpkinMenu.SetBannerType(banner);

            AddPlayerMenu(pumpkinMenu);
            AddTimeMenu(pumpkinMenu);
            AddWeatherMenu(pumpkinMenu);
            AddWorldMenu(pumpkinMenu);

            UI.Notify("~o~PumpkinMenu~w~ loaded successfully. Press ~b~F5~w~ to open.");
        }

        private void AddPlayerMenu(UIMenu mainMenu)
        {
            UIMenu playerMenu = menuPool.AddSubMenu(mainMenu, "Player", "Change your character");
            playerMenu.AddItem(new UIMenuCheckboxItem("Godmode", false));
            AddWantedLevelSubmenu();

            playerMenu.OnCheckboxChange += (sender, item, index) =>
            {
                if (item.Text == "Godmode")
                {
                    Function.Call(Hash.SET_PLAYER_INVINCIBLE, Game.Player, index);
                    checkboxNotify(item.Text, index);
                }
            };

            #region Submenues
            void AddWantedLevelSubmenu()
            {
                UIMenu manageWantedLevelSubmenu = menuPool.AddSubMenu(playerMenu, "Manage wanted level");
                manageWantedLevelSubmenu.AddItem(new UIMenuItem("Clear wanted level"));
                manageWantedLevelSubmenu.AddItem(new UIMenuItem("Increase wanted level"));
                manageWantedLevelSubmenu.AddItem(new UIMenuItem("Decrease wanted level"));
                manageWantedLevelSubmenu.OnItemSelect += (sender, item, index) =>
                {
                    switch (index)
                    {
                        case 0:
                            Function.Call(Hash.CLEAR_PLAYER_WANTED_LEVEL, Game.Player);
                            break;
                        case 1:
                            if (Game.Player.WantedLevel < 5)
                                Game.Player.WantedLevel++;
                            break;
                        case 2:
                            if (Game.Player.WantedLevel > 0)
                                Game.Player.WantedLevel--;
                            break;
                        default:
                            break;
                    };
                };
            }
            #endregion
        }

        private void AddTimeMenu(UIMenu mainMenu)
        {
            var times = new List<string>
            {
                "Move 1 hour forward",
                "Move 1 hour backward",
                "Morning",
                "Midday",
                "Evening",
                "Midnight"
            };
            UIMenu timeMenu = menuPool.AddSubMenu(mainMenu, "Time", "Change time");

            timeMenu.AddItem(new UIMenuCheckboxItem("Freeze Time", false));
            foreach (string time in times)
                timeMenu.AddItem(new UIMenuItem(time));

            timeMenu.OnCheckboxChange += (sender, item, index) =>
            {
                if (item.Text == "Freeze Time")
                {
                    Function.Call(Hash.PAUSE_CLOCK, index);
                    checkboxNotify(item.Text, index);
                }
            };

            timeMenu.OnItemSelect += (sender, item, index) =>
            {
                switch (index)
                {
                    case 1:
                        Function.Call(Hash.ADD_TO_CLOCK_TIME, 1, 00, 00);
                        break;
                    case 2:
                        UI.Notify("~o~PumpkinMenu~w~: ~r~Not available yet :(");
                        break;
                    case 3:
                        Function.Call(Hash.SET_CLOCK_TIME, 7, 00, 00);
                        break;
                    case 4:
                        Function.Call(Hash.SET_CLOCK_TIME, 12, 00, 00);
                        break;
                    case 5:
                        Function.Call(Hash.SET_CLOCK_TIME, 18, 00, 00);
                        break;
                    case 6:
                        Function.Call(Hash.SET_CLOCK_TIME, 00, 00, 00);
                        break;
                    default:
                        break;
                };
            };
        }

        private void AddWeatherMenu(UIMenu mainMenu)
        {
            var weathers = new List<string>
            {
                "Extra Sunny",
                "Clear",
                "Clouds",
                "Foggy",
                "Raining",
                "ThunderStorm",
                "Snowing",
                "Blizzard",
                "Chirstmas",
                "Halloween"
            };
            UIMenu weatherMenu = menuPool.AddSubMenu(mainMenu, "Weather", "Change weather");
            foreach (string weather in weathers)
                weatherMenu.AddItem(new UIMenuItem(weather));

            weatherMenu.OnItemSelect += (sender, item, index) =>
            {
                switch (index)
                {
                    case 0:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.ExtraSunny);
                        World.Weather = Weather.ExtraSunny;
                        break;
                    case 1:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Clear);
                        World.Weather = Weather.Clear;
                        break;
                    case 2:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Clouds);
                        World.Weather = Weather.Clouds;
                        break;
                    case 3:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Foggy);
                        World.Weather = Weather.Foggy;
                        break;
                    case 4:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Raining);
                        World.Weather = Weather.Raining;
                        break;
                    case 5:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.ThunderStorm);
                        World.Weather = Weather.ThunderStorm;
                        break;
                    case 6:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Snowing);
                        World.Weather = Weather.Snowing;
                        break;
                    case 7:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Blizzard);
                        World.Weather = Weather.Blizzard;
                        break;
                    case 8:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Christmas);
                        World.Weather = Weather.Christmas;
                        break;
                    case 9:
                        //Function.Call(Hash.SET_WEATHER_TYPE_NOW, (int)Weather.Halloween);
                        World.Weather = Weather.Halloween;
                        break;
                    default:
                        break;
                };
            };
        }

        private void AddWorldMenu(UIMenu mainMenu)
        {
            UIMenu worldMenu = menuPool.AddSubMenu(mainMenu, "World");
            worldMenu.AddItem(new UIMenuCheckboxItem("Blackout", false));
            worldMenu.OnCheckboxChange += (sender, item, index) =>
            {
                if (item.Text == "Blackout")
                {
                    World.SetBlackout(index);
                    checkboxNotify(item.Text, index);
                }
            };
        }
        #endregion

        private void checkboxNotify(string text, bool enabled)
        {
            UI.Notify("~o~PumpkinMenu~w~: " + text + " " + (enabled ? "~g~enabled" : "~r~disabled"));
        }
    }
}
