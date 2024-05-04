using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin_test.Classes;
using Xamarin_test.Views;
using Xamarin_test.Models;

namespace Xamarin_test.ViewModels
{
    public class AimsViewModel : BaseViewModel
    {
        private Purpose _selectedAim;

        Node<abstract_Item> root;
        public Command LoginCommand { get; }

        public AimsViewModel()
        {
            LoginCommand = new Command(OnAimSelected);
        }
        public Purpose SelectedAim
        {
            get => _selectedAim;
            set
            {
                SetProperty(ref _selectedAim, value);
                OnAimSelected(value);
            }
        }
        private async void OnAimSelected(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AimEditPage)}?{nameof(AimEditViewModel)}={_selectedAim}");
        }
    }
}
