using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Common.Interfaces;

namespace Arena.Views
{
    public partial class PlayerPresentationUserControl
    {
        private List<ICompetitor> _competitors;
        private IEnumerator<ICompetitor> _enumeratedCompetitors;
        private const int TIMEFORPRESENTINGONEPLAYER = 5;
        private System.Timers.Timer _timer;

        public PlayerPresentationUserControl()
        {
            InitializeComponent();
        }

        private void PlayerPresentationUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProgressBar1.Maximum = TIMEFORPRESENTINGONEPLAYER * 100;
            _timer = new System.Timers.Timer
            {
                Interval = 10
            };

            _competitors = (List<ICompetitor>)DataContext;

            if (_competitors != null)
            {
                _enumeratedCompetitors = _competitors.GetEnumerator();
            }
        }

        private void StartCounting()
        {
            ProgressBar1.Value = 0;
            _timer.Start();
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ProgressBar1.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action( () => ProgressBar1.Value++));
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerPresenter.Visibility = Visibility.Collapsed;
            _timer.Elapsed -= OnTimedEvent;
        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            _timer.Elapsed += OnTimedEvent;

            StartCounting();
        }

        private void PlayerPresentationUserControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ProgressBar1.Value = 0;
                if (_enumeratedCompetitors != null)
                {
                    _enumeratedCompetitors.Reset();
                    if (_enumeratedCompetitors.MoveNext())
                    {
                        CurrentViewedPlayerUserControl.DataContext = _enumeratedCompetitors.Current;
                    }
                    CurrentViewedPlayerUserControl.DataContext = _enumeratedCompetitors.Current;
                }
            }
            else
            {
                if (_timer != null)
                {
                    _timer.Elapsed -= OnTimedEvent;
                }
            }
        }

        private void ProgressBar1_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ProgressBar1.Value >= TIMEFORPRESENTINGONEPLAYER * 100)
            {
                if (_enumeratedCompetitors.MoveNext())
                {
                    CurrentViewedPlayerUserControl.DataContext = _enumeratedCompetitors.Current;
                    StartCounting();
                }
                else
                {
                    PlayerPresenter.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
