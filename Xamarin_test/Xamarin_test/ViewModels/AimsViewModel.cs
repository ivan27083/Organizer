using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin_test.Classes;
using Xamarin_test.Views;
using Xamarin_test.Models;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin_test.Services;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Xamarin_test.ViewModels
{
    public class AimsViewModel : BaseViewModel
    {
        public IDataStore<Purpose> DataStoreAims => DependencyService.Get<IDataStore<Purpose>>();
        public IDataStore<Mission> DataStoreMissions => DependencyService.Get<IDataStore<Mission>>();

        private Purpose _selectedAim;
        bool showFill = true;
        public Node<abstract_Item> root;
        public Node<abstract_Item> current;
        public Command LoginCommand { get; }
        public AimsViewModel()
        {
            Title = "Aims";
            LoginCommand = new Command(OnAimSelected);
            fillTree();
        }
        public async void fillTree()
        {
            List<Purpose> purposes = new List<Purpose>();
            var aims = await DataStoreAims.GetItemsAsync(true);
            foreach (var aim in aims)
            {
                purposes.Add(aim);
            }
            List<Mission> missions = new List<Mission>();
            var items = await DataStoreMissions.GetItemsAsync(true);
            foreach (var item in items)
            {
                if (item.Group != null) missions.Add(item);
            }
            if (purposes.Count != 0)
            {
                root = new Node<abstract_Item>(purposes.Find(p => p.Group == 0));
                current = root;
            }
            while (purposes.Count != 0)
            {
                var el = purposes.Where(p => p.Group == current.children_group);
                if (el.Any())
                {
                    foreach (var item in el)
                    {
                        current.AddChild(new Node<abstract_Item>(item));
                        current = current.children.Last();
                        purposes.Remove(item);
                    }
                }
                else
                {
                    current = current.parent;
                }
            }
            current = root;
            while (missions.Count != 0)
            {
                var el = missions.Where(m => m.Group == current.children_group);
                if (el.Any())
                {
                    foreach (var item in el)
                    {
                        current.AddChild(new Node<abstract_Item>(item));
                        current = current.children.Last();
                        missions.Remove(item);
                    }
                }
                else
                {
                    current = current.parent;
                }
            }
            current = root;
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
