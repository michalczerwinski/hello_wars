using System;
using System.Windows;
using Game.AntWars.Models.BaseUnits;
using Game.AntWars.ViewModels.BaseUnits;

namespace Game.AntWars.UserControls.BaseUnits
{
    public partial class AntUserControl
    {
        private AntViewModel ViewModel
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

        private void AttackAnimation_OnCompleted(object sender, EventArgs e)
        {
            ViewModel.IsAttackingAnimationCompleated = true;
        }

}}
