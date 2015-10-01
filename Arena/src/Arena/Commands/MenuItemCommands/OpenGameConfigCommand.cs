using Arena.ViewModels;
using Common.Utilities;
using Microsoft.Win32;

namespace Arena.Commands.MenuItemCommands
{
    class OpenGameConfigCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public OpenGameConfigCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "XML Files (*.xml)|*.xml",
                Multiselect = false
            };
            var result = dlg.ShowDialog();

            if (result == true)
            {
                var filePath = dlg.FileName;
                _viewModel.ApplyGameCustomConfiguration(filePath);
            }
        }
    }
}
