using System;
using System.Windows;
using Game.AntWars.Models.BaseUnits;
using Game.AntWars.ViewModels.BaseUnits;

namespace Game.AntWars.UserControls.Units
{
    public partial class AntUserControl
    {
        private AntViewModel _viewModel
        {
            get
            {
                var antModel = (AntModel) DataContext;
                return antModel.ViewModel;
            }
        }

        public AntUserControl()
        {
            InitializeComponent();
        }

        private void ConnectAnimationEffect_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MoveAnimation_OnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExplodeAnimation_OnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AttackAnimation_OnCompleted(object sender, EventArgs e)
        {
            _viewModel.IsAttackingAnimationCompleated = true;
        }

        private void ShadowMoveAnimation_OnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
}}
