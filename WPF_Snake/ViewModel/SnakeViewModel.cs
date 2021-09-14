using System;
using System.Collections.ObjectModel;
using WPF_Snake.Model;

namespace WPF_Snake.ViewModel
{
    public class SnakeViewModel : ViewModelBase
    {
        private SnakeGameModel _model;
        private Int32 _eggCount;
        public Int32 EggCount
        {
            get 
            {
                return _eggCount;
            }
            set 
            {
                _eggCount = value;
                OnPropertyChanged("EggCount");
            }
        }
        public string PauseText { get; set; }
        public Int32 mapN { get; private set; }
        public DelegateCommand QuitCommand { get; private set; }
        public DelegateCommand NewGame11Command { get; private set; }
        public DelegateCommand NewGame15Command { get; private set; }
        public DelegateCommand NewGame23Command { get; private set; }
        public DelegateCommand StartPauseCommand { get; private set; }

        public ObservableCollection<SnakeField> Map { get; set; }

        #region Events
        public event EventHandler QuitGameEvent;
        public event EventHandler NewGameEvent;
        public event EventHandler PauseEvent;
        #endregion
        public SnakeViewModel(SnakeGameModel argModel)
        {
            _model = argModel;
            _model.EggEaten += UpdateEggCount;
            _model.GameAdvanced += OnGameAdvanced;
            //Setup Commands
            QuitCommand = new DelegateCommand(param => OnQuitGame());
            NewGame11Command = new DelegateCommand(param => On11Game());
            NewGame15Command = new DelegateCommand(param => On15Game());
            NewGame23Command = new DelegateCommand(param => On23Game());
            StartPauseCommand = new DelegateCommand(param => OnStartPause());
            PauseText = "-----";
        }

        private void OnQuitGame()
        {
            if (QuitGameEvent != null)
            {
                QuitGameEvent(this, EventArgs.Empty);
            }
        }
        private async void NewGame(String argPath)
        {

            await _model.LoadGameAsync(argPath);
            mapN = _model.GetTable().GetMapSize();
            //_model.InitializeTable(mapN);
            //
            PauseText = "Start";
            OnPropertyChanged("PauseText");
            //
            GenerateMap();
            if (NewGameEvent != null)
            {
                NewGameEvent(this, EventArgs.Empty);
            }
            //
            EggCount = 0;
            OnPropertyChanged("EggCount");
            //
            UpdateView();
        }
        private void On11Game()
        {
            NewGame(@"..\..\..\Maps\n11_map.txt");
            
        }
        private void On15Game()
        {
            NewGame(@"..\..\..\Maps\n15_map.txt");
        }
        private void On23Game()
        {
            NewGame(@"..\..\..\Maps\n23_map.txt");
        }
        private void GenerateMap()
        {
            Map = new ObservableCollection<SnakeField>();
            for (int i = 0; i < mapN; i++)
            {
                for (int j = 0; j < mapN; j++)
                {
                   Map.Add(new SnakeField
                    {
                        Number=0,
                        X = i,
                        Y = j
                    }) ;
                }
            }
        }
        public void UpdateView()
        {
            foreach(SnakeField field in Map)
            {
                field.Number = _model.GetTable().GetMapValue(field.X, field.Y);
            }
        }
        private void UpdateEggCount(object sender, EventArgs e)
        {
            EggCount = _model.GetEggCount();
        }
        private void OnGameAdvanced(object sender, EventArgs e)
        {
            UpdateView();
        }
        private void OnStartPause()
        {
            if (PauseText == "Start")
            {
                PauseText = "Pause";
            }
            else
            {
                PauseText = "Start";
            }
            OnPropertyChanged("PauseText");
            PauseEvent(this, EventArgs.Empty);
        }
        public void RestartGameOver()
        {
            switch (_model.GetTable().GetMapSize())
            {
                case 11:
                    NewGame(@"..\..\..\Maps\n11_map.txt");
                    break;
                case 15:
                    NewGame(@"..\..\..\Maps\n15_map.txt");
                    break;
                case 23:
                    NewGame(@"..\..\..\Maps\n23_map.txt");
                    break;
                default:
                    break;
            }
        }
    }
}