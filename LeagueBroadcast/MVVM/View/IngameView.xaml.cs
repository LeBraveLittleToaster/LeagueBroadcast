using LeagueBroadcast.Common;
using LeagueBroadcast.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LeagueBroadcast.OperatingSystem;

namespace LeagueBroadcast.MVVM.View
{
    /// <summary>
    /// Interaction logic for IngameView.xaml
    /// </summary>
    public partial class IngameView : UserControl
    {
        private IngameViewModel ctx;

        private int idAlt;
        private int idControl;
        
        public IngameView()
        {
            InitializeComponent();
            
            OpenContent.Width = 360;
            OpenContent.Opacity = 0;


            DataContextChanged += (s, e) => {
                ctx = (IngameViewModel)e.NewValue;
                //Objectives
                ObjectivePanel.DataContext = ctx.Objectives;
                BaronButton.DataContext = ctx.Objectives.BaronTimer;
                DragonButton.DataContext = ctx.Objectives.ElderTimer;
                InhibButton.DataContext = ctx.Objectives.InhibTimer;
                ObjectiveSpawnButton.DataContext = ctx.Objectives.ObjectiveSpawn;
                ObjectiveKillButton.DataContext = ctx.Objectives.ObjectiveKill;

                //Teams
                TeamPanel.DataContext = ctx.Teams;
                TeamNamesButton.DataContext = ctx.Teams.Name;
                TeamScoresButton.DataContext = ctx.Teams.Score;
                TeamIconsButton.DataContext = ctx.Teams.Icon;
                GoldGraphButton.DataContext = ctx.Teams.Gold;
                CustomScoreboardButton.DataContext = ctx.Teams.Scoreboard;

                //Players
                PlayerPanel.DataContext = ctx.Players;
                ItemsButton.DataContext = ctx.Players.Items;
                LevelUpButton.DataContext = ctx.Players.LevelUp;
                EXPButton.DataContext = ctx.Players.EXP;
                PlayerGoldButton.DataContext = ctx.Players.PlayerGold;
                PlayerCSpMButton.DataContext = ctx.Players.PlayerCSperMin;
            };
            Loaded += RegisterHotkeys;
            Unloaded += UnregisterHotkeys;
        }

        private void UnregisterHotkeys(object sender, RoutedEventArgs e)
        {
            GlobalHotKey.UnregisterHotKey(idAlt);
            GlobalHotKey.UnregisterHotKey(idControl);
            Console.Out.WriteLine("Unregistered");
        }

        private void RegisterHotkeys(object sender, RoutedEventArgs e)
        {
            idAlt = GlobalHotKey.RegisterHotKey(ModifierKeys.Alt, Key.A, () =>
            {
                Console.Out.WriteLine("Hello World ALT");
            });
            idControl = GlobalHotKey.RegisterHotKey(ModifierKeys.Control, Key.A, () => ctx.Teams.ScoreboardButtonCommand.Execute(new object()));
            Console.Out.WriteLine("Registered");
        }

        private void MainContainer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            /*
            if(e.VerticalChange > 0 && ctx.TeamsIsOpen == false)
            {
                ctx.TeamsIsOpen = true; 
            }

            if(e.VerticalChange < 0 && ctx.TeamsIsOpen == true)
            {
                ctx.TeamsIsOpen = false;
            }
            */
        }
    }
}
