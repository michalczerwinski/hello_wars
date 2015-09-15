using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Configuration;
using Arena.ViewModels;
using Common.Serialization;
using Microsoft.Win32;

namespace Arena.Commands.MenuItemCommands
{
    class OpenCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public OpenCommand(MainWindowViewModel viewModel)
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
                _viewModel.ApplyConfiguration(filePath);
                _viewModel.OnLoadedCommand.Execute(null);
            }
        }
    }
}
