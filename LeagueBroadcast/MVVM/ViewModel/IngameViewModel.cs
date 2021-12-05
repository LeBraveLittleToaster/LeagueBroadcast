using System;
using System.Windows.Controls;
using LeagueBroadcast.Common;
using LeagueBroadcast.Common.Controllers;
using LeagueBroadcast.MVVM.Core;
using System.Windows.Input;
using KeyDownTester.Keys;
using LeagueBroadcast.MVVM.Models;

namespace LeagueBroadcast.MVVM.ViewModel
{
    class IngameViewModel : ObservableObject
    {
        public ObjectivesTabViewModel Objectives = new();
        public PlayersTabViewModel Players = new();
        public TeamsTabViewModel Teams = new();

        public IngameTeamsViewModel IngameTeamsVM { get; set; }

        public bool IsActive
        {
            get { return ConfigController.Component.Ingame.IsActive; }
            //TODO Update App state on change
            set { ConfigController.Component.Ingame.IsActive = value; OnPropertyChanged(); }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; OnPropertyChanged(); }
        }

        private DelegateCommand _openCommand;

        public DelegateCommand OpenCommand
        {
            get { return _openCommand; }
        }

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; }
        }

        private bool _teamsIsOpen;

        public bool TeamsIsOpen
        {
            get { return _teamsIsOpen; }
            set { _teamsIsOpen = value; OnPropertyChanged(); }
        }

        private DelegateCommand _teamsButtonCommand;

        public DelegateCommand TeamsButtonCommand
        {
            get { return _teamsButtonCommand; }
            set { _teamsButtonCommand = value; }
        }


        public IngameViewModel()
        {
            _openCommand = new(o => { 
                IsOpen = true; 
                BroadcastController.Instance.Main.SetIngameSelected();
                MainViewModel.HomeVM.InfoButtonIsVisible = false;
                MainViewModel.HomeVM.InfoIsOpen = false;
            });
            _openCommand.MouseGesture = MouseAction.LeftClick;

            _closeCommand = new(o => { IsOpen = false; });
            _closeCommand.GestureKey = Key.Escape;

            _teamsButtonCommand = new(o => { TeamsIsOpen ^= true; Log.Verbose("Switch Team View visibility"); });
            _teamsButtonCommand.MouseGesture = MouseAction.LeftClick;

            IngameTeamsVM = new();
        }
    }

    public class ObjectivesTabViewModel : ObservableObject
    {
        public ControlButtonViewModel BaronTimer = new("Baron Timer", "Show Timer and Gold difference when Baron is taken", ConfigController.Component.Ingame.UseLiveEvents,new HotKey(Key.NumPad7));
        public ControlButtonViewModel ElderTimer = new("Elder Timer", "Show Timer and Gold difference when Elder Drake is taken", ConfigController.Component.Ingame.UseLiveEvents,new HotKey(Key.NumPad4));
        public ControlButtonViewModel InhibTimer = new("Inhibitor Timers", "Show Inhibitor Timers", true,new HotKey(Key.NumPad1));
        public ControlButtonViewModel ObjectiveSpawn = new("Spawn", "Show Spawn PopUp when an objective spawns", ConfigController.Component.Ingame.UseLiveEvents,new HotKey(Key.NumPad4, ModifierKeys.Control));
        public ControlButtonViewModel ObjectiveKill = new("Kill", "Show Spawn PopUp when an objective is killed", ConfigController.Component.Ingame.UseLiveEvents,new HotKey(Key.NumPad1, ModifierKeys.Control));

        public bool BaronIsActive { get { return ConfigController.Component.Ingame.Objectives.DoBaronKill; } set { ConfigController.Component.Ingame.Objectives.DoBaronKill = value; OnPropertyChanged(); } }
        public bool ElderIsActive { get { return ConfigController.Component.Ingame.Objectives.DoDragonKill; } set { ConfigController.Component.Ingame.Objectives.DoDragonKill = value; OnPropertyChanged(); } }
        public bool InhibIsActive { get { return ConfigController.Component.Ingame.Objectives.DoInhibitors; } set { ConfigController.Component.Ingame.Objectives.DoInhibitors = value; OnPropertyChanged(); } }
        public bool ObjectiveSpawnPopUpIsActive { get { return ConfigController.Component.Ingame.Objectives.DoObjectiveSpawnPopUp; } set { ConfigController.Component.Ingame.Objectives.DoObjectiveSpawnPopUp = value; OnPropertyChanged(); } }
        public bool ObjectiveKillPopUpIsActive { get { return ConfigController.Component.Ingame.Objectives.DoObjectiveKillPopUp; } set { ConfigController.Component.Ingame.Objectives.DoObjectiveKillPopUp = value; OnPropertyChanged(); } }

        private DelegateCommand _baronClickCommand;

        public DelegateCommand BaronClickCommand
        {
            get { return _baronClickCommand; }
            set { _baronClickCommand = value; }
        }

        private DelegateCommand _dragonClickCommand;

        public DelegateCommand DragonClickCommand
        {
            get { return _dragonClickCommand; }
            set { _dragonClickCommand = value; }
        }

        private DelegateCommand _inhibClickCommand;

        public DelegateCommand InhibClickCommand
        {
            get { return _inhibClickCommand; }
            set { _inhibClickCommand = value; }
        }

        private DelegateCommand _objectiveSpawnClickCommand;

        public DelegateCommand ObjectiveSpawnClickCommand
        {
            get { return _objectiveSpawnClickCommand; }
            set { _objectiveSpawnClickCommand = value; }
        }

        private DelegateCommand _objectiveKillClickCommand;

        public DelegateCommand ObjectiveKillClickCommand
        {
            get { return _objectiveKillClickCommand; }
            set { _objectiveKillClickCommand = value; }
        }


        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }


        public ObjectivesTabViewModel()
        {
            _baronClickCommand = new(o => {
                BaronIsActive ^= true;
            });
            _baronClickCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(BaronTimer.Hotkey.Modifiers,BaronTimer.Hotkey.Key,() => _baronClickCommand.Execute(new object()));

            _dragonClickCommand = new(o => {
                ElderIsActive ^= true;
            });
            _dragonClickCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(ElderTimer.Hotkey.Modifiers,ElderTimer.Hotkey.Key,() => _dragonClickCommand.Execute(new object()));

            _inhibClickCommand = new(o => {
                InhibIsActive ^= true;
            });
            _inhibClickCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(InhibTimer.Hotkey.Modifiers,InhibTimer.Hotkey.Key,() => _inhibClickCommand.Execute(new object()));

            _objectiveKillClickCommand = new(o =>
            {
                ObjectiveKillPopUpIsActive ^= true;
            });
            _objectiveKillClickCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(ObjectiveKill.Hotkey.Modifiers,ObjectiveKill.Hotkey.Key,() => _objectiveKillClickCommand.Execute(new object()));


            _objectiveSpawnClickCommand = new(o =>
            {
                ObjectiveSpawnPopUpIsActive ^= true;
            });
            _objectiveSpawnClickCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(ObjectiveSpawn.Hotkey.Modifiers,ObjectiveSpawn.Hotkey.Key,() => _objectiveSpawnClickCommand.Execute(new object()));
        }
    }

    public class PlayersTabViewModel : ObservableObject
    {
        public ControlButtonViewModel Items = new("Item Notifications", "Show Item pop up over player icons when item is completed", ConfigController.Component.Ingame.DoItemCompleted, new HotKey(Key.NumPad8));
        public ControlButtonViewModel LevelUp = new("Level Up Notifications", "Show Lvl Up pop up @ lvls 6/11/16", ConfigController.Component.Ingame.DoLevelUp,new HotKey(Key.NumPad5));
        public ControlButtonViewModel EXP = new("EXP Tab", "EXP and Level per Player scoreboard", true,new HotKey(Key.NumPad2));
        public ControlButtonViewModel PlayerGold = new("Gold Tab", "Gold per Player scoreboard", true,new HotKey(Key.NumPad5, ModifierKeys.Control));
        public ControlButtonViewModel PlayerCSperMin = new("CS per Min.", "CS per Minute per Player scoreboard", true,new HotKey(Key.NumPad2, ModifierKeys.Control));

        public bool ItemsIsActive { get { return ConfigController.Component.Ingame.DoItemCompleted; } set { ConfigController.Component.Ingame.DoItemCompleted = value; OnPropertyChanged(); } }
        public bool LevelUpIsActive { get { return ConfigController.Component.Ingame.DoLevelUp; } set { ConfigController.Component.Ingame.DoLevelUp = value; OnPropertyChanged(); } }
        public bool EXPIsActive { get { return IngameController.CurrentSettings.EXP; } set { IngameController.CurrentSettings.EXP = value; OnPropertyChanged(); } }
        public bool PlayerGoldIsActive { get { return IngameController.CurrentSettings.PlayerGold; } set { IngameController.CurrentSettings.PlayerGold = value; OnPropertyChanged(); } }
        public bool PlayerCSPerMinIsActive { get { return IngameController.CurrentSettings.CSPerMin; } set { IngameController.CurrentSettings.CSPerMin = value; OnPropertyChanged(); } }

        private DelegateCommand _itemsButtonCommand;

        public DelegateCommand ItemsButtonCommand
        {
            get { return _itemsButtonCommand; }
            set { _itemsButtonCommand = value; }
        }

        private DelegateCommand _levelUpButtonCommand;

        public DelegateCommand LevelUpButtonCommand
        {
            get { return _levelUpButtonCommand; }
            set { _levelUpButtonCommand = value; }
        }

        private DelegateCommand _expButtonCommand;

        public DelegateCommand ExpButtonCommand
        {
            get { return _expButtonCommand; }
            set { _expButtonCommand = value; }
        }

        private DelegateCommand _playerGoldButtonCommand;

        public DelegateCommand PlayerGoldButtonCommand
        {
            get { return _playerGoldButtonCommand; }
            set { _playerGoldButtonCommand = value; }
        }

        private DelegateCommand _csPerMinButtonCommand;

        public DelegateCommand CsPerMinButtonCommand
        {
            get { return _csPerMinButtonCommand; }
            set { _csPerMinButtonCommand = value; }
        }


        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
            set { _isOpen = value; }
        }

        public PlayersTabViewModel()
        {
            _itemsButtonCommand = new(o => {
                ItemsIsActive ^= true;
            });
            _itemsButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Items.Hotkey.Modifiers,Items.Hotkey.Key,() => _itemsButtonCommand.Execute(new object()));


            _levelUpButtonCommand = new(o => {
                LevelUpIsActive ^= true;
            });
            _levelUpButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(LevelUp.Hotkey.Modifiers,LevelUp.Hotkey.Key,() => _levelUpButtonCommand.Execute(new object()));


            _expButtonCommand = new(o => {
                EXPIsActive ^= true;
            });
            _expButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(EXP.Hotkey.Modifiers,EXP.Hotkey.Key,() => _expButtonCommand.Execute(new object()));


            _playerGoldButtonCommand = new(o => {
                PlayerGoldIsActive ^= true;
            });
            _playerGoldButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(PlayerGold.Hotkey.Modifiers,PlayerGold.Hotkey.Key,() => _playerGoldButtonCommand.Execute(new object()));


            _csPerMinButtonCommand = new(o => {
                PlayerCSPerMinIsActive ^= true;
            });
            _csPerMinButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(PlayerCSperMin.Hotkey.Modifiers,PlayerCSperMin.Hotkey.Key,() => _csPerMinButtonCommand.Execute(new object()));

        }
    }

    public class TeamsTabViewModel : ObservableObject
    {
        public ControlButtonViewModel Scoreboard = new("Scoreboard", "Use custom scoreboard", ConfigController.Component.Ingame.UseCustomScoreboard,new HotKey(Key.NumPad9));
        public ControlButtonViewModel Name = new("Team Names", "Show Item pop up over player icons when item is completed", ConfigController.Component.Ingame.Teams.DoTeamNames, new HotKey(Key.NumPad6));
        public ControlButtonViewModel Score = new("Team Scores", "Show Item pop up over player icons when item is completed", ConfigController.Component.Ingame.Teams.DoTeamScores,new HotKey(Key.NumPad3));
        public ControlButtonViewModel Icon = new("Team Icons", "Show Item pop up over player icons when item is completed", ConfigController.Component.Ingame.Teams.DoTeamIcons,new HotKey(Key.NumPad6, ModifierKeys.Control));
        public ControlButtonViewModel Gold = new("Gold Graph", "Scoreboard team gold difference graph since the start of the game", IngameController.CurrentSettings.GoldGraph,new HotKey(Key.NumPad3, ModifierKeys.Control));

        
        public bool NamesIsActive { get { return ConfigController.Component.Ingame.Teams.DoTeamNames; } set{ ConfigController.Component.Ingame.Teams.DoTeamNames = value; OnPropertyChanged(); } }
        public bool ScoresIsActive { get { return ConfigController.Component.Ingame.Teams.DoTeamScores; } set { ConfigController.Component.Ingame.Teams.DoTeamScores = value; OnPropertyChanged(); } }
        public bool IconsIsActive { get { return ConfigController.Component.Ingame.Teams.DoTeamIcons; } set { ConfigController.Component.Ingame.Teams.DoTeamIcons = value; OnPropertyChanged(); } }
        public bool ScoreboardIsActive { get { return ConfigController.Component.Ingame.UseCustomScoreboard; } set { ConfigController.Component.Ingame.UseCustomScoreboard = value; OnPropertyChanged(); } }

        public bool GoldGraphIsActive { get { return IngameController.CurrentSettings.GoldGraph; } set { IngameController.CurrentSettings.GoldGraph = value; OnPropertyChanged(); } }

        
        private DelegateCommand _namesButtonCommand;

        public DelegateCommand NamesButtonCommand
        {
            get { return _namesButtonCommand; }
            set { _namesButtonCommand = value; }
        }

        private DelegateCommand _scoresButtonCommand;

        public DelegateCommand ScoresButtonCommand
        {
            get { return _scoresButtonCommand; }
            set { _scoresButtonCommand = value; }
        }

        private DelegateCommand _scoreboardButtonCommand;

        public DelegateCommand ScoreboardButtonCommand
        {
            get { return _scoreboardButtonCommand; }
            set { _scoreboardButtonCommand = value; }
        }

        private DelegateCommand _iconsButtonCommand;

        public DelegateCommand IconsButtonCommand
        {
            get { return _iconsButtonCommand; }
            set { _iconsButtonCommand = value; }
        }

        private DelegateCommand _goldGraphButtonCommand;

        public DelegateCommand GoldGraphButtonCommand
        {
            get { return _goldGraphButtonCommand; }
            set { _goldGraphButtonCommand = value; }
        }


        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }


        public TeamsTabViewModel()
        {
            _namesButtonCommand = new(o => {
                NamesIsActive ^= true;
            });
            _namesButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Name.Hotkey.Modifiers,Name.Hotkey.Key,() => _namesButtonCommand.Execute(new object()));

            _scoresButtonCommand = new(o => {
                ScoresIsActive ^= true;
            });
            _scoresButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Score.Hotkey.Modifiers,Score.Hotkey.Key,() => _scoresButtonCommand.Execute(new object()));

            _iconsButtonCommand = new(o => {
                IconsIsActive ^= true;
            });
            _iconsButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Icon.Hotkey.Modifiers,Icon.Hotkey.Key,() => _iconsButtonCommand.Execute(new object()));

            _goldGraphButtonCommand = new(o => {
                GoldGraphIsActive ^= true;
            });
            _goldGraphButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Gold.Hotkey.Modifiers,Gold.Hotkey.Key,() => _goldGraphButtonCommand.Execute(new object()));

            _scoreboardButtonCommand = new(o => {
                ScoreboardIsActive ^= true;
            });
            _scoreboardButtonCommand.MouseGesture = MouseAction.LeftClick;
            HotkeysManager.AddHotkey(Scoreboard.Hotkey.Modifiers,Scoreboard.Hotkey.Key,() => _scoreboardButtonCommand.Execute(new object()));
        }

    }

    public class ControlButtonViewModel : ObservableObject
    {

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; OnPropertyChanged(); }
        }
        
        private HotKey _hotkey;

        public HotKey Hotkey
        {
            get { return _hotkey; }
            set { _hotkey = value; OnPropertyChanged(); }
        }

        public ControlButtonViewModel(string title, string desc, bool isEnabled, HotKey hotkey)
        {
            this._isEnabled = isEnabled;
            this._title = title;
            this._description = desc;
            this._hotkey = hotkey;
        }

    }
}
