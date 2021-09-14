using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using WPF_Snake.ViewModel;
using WPF_Snake.Persistence;
using WPF_Snake.Model;

namespace WPF_Snake
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Variables
        private SnakeGameModel _model;
        private MainWindow _view;
        private SnakeViewModel _viewModel;
        private DispatcherTimer _timer;
        #endregion

        #region Constructor
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        #endregion

        #region Private Functions
        private void App_Startup(object sender, StartupEventArgs e)
        {
            //Initialize Model
            _model = new SnakeGameModel(new SnakeFileDataAccess());
            _model.GameOver += HandleGameOver;
            //Initialize ViewModel
            _viewModel = new SnakeViewModel(_model);
            _viewModel.QuitGameEvent += new EventHandler(ViewModel_QuitGame);
            _viewModel.NewGameEvent += new EventHandler(NewGameHandler);
            _viewModel.PauseEvent += new EventHandler(ViewModel_PauseEvent);
            //Initialize View
            _view = new MainWindow();
            _view.PauseButton.IsEnabled = false;
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing);
            _view.KeyDown += KeyInputHandler;
            _view.Show();
            //Iniate Timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(0.4);
            _timer.Tick += new EventHandler(Tick);
            //Key Input
        }
        private void Tick(object sender, EventArgs e)
        {
            _model.AdvanceGame();
            _view.DataContext = _viewModel;
        }

        private void View_Closing(object sender, CancelEventArgs e)
        {
            _timer.Stop();
            if (MessageBox.Show("Are you sure you wish to quit", "Snake", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
                _timer.Start();
            }
        }
        private void ViewModel_QuitGame(object sender, System.EventArgs e)
        {
            _view.Close();
        }
        private void NewGameHandler(object sender, System.EventArgs e)
        {
            _view.ViewMap.ItemsSource = _viewModel.Map;
            _view.PauseButton.IsEnabled = true;
            if(_timer.IsEnabled)
            {
                _timer.Stop();
            }
        }
        private void ViewModel_PauseEvent(object sender, System.EventArgs e)
        {
            if(_timer != null && _timer.IsEnabled)
            {
                _timer.Stop();
            }
            else if(_timer!= null && !_timer.IsEnabled)
            {
                _timer.Start();
            }
        }

        private void KeyInputHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _model.GetSnake().ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    _model.GetSnake().ChangeDirection(Direction.Down);
                    break;
                case Key.Right:
                    _model.GetSnake().ChangeDirection(Direction.Right);
                    break;
                case Key.Left:
                    _model.GetSnake().ChangeDirection(Direction.Left);
                    break;
                default:
                    break;
            }
        }
        private void HandleGameOver(object sender, EventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Eggs Eaten: " + _model.GetEggCount().ToString());
            _viewModel.RestartGameOver();
        }
        #endregion

    }
}
