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
using Xamarin.Essentials;

namespace Xamarin_test.ViewModels
{
    public class AimsViewModel : BaseViewModel
    {
        public IDataStore<Purpose> DataStoreAims => DependencyService.Get<IDataStore<Purpose>>();
        public IDataStore<Mission> DataStoreMissions => DependencyService.Get<IDataStore<Mission>>();
        private IGlobalTouch service = DependencyService.Get<IGlobalTouch>();

        private Purpose _selectedAim;
        public string text;
        public string description;

        bool showFill = true;
        public Node<abstract_Item> root;
        public Node<abstract_Item> current;
        public List<Circle> circles = new List<Circle>();
        public Command ChangeCommand { get; }
        public Command AddCommand { get; }
        public Command AddMissionCommand { get; }
        public AimsViewModel()
        {
            Title = "Aims";
            text = "";
            description = "";
            ChangeCommand = new Command(OnAimSelected);
            AddCommand = new Command(OnAddAim);
            AddMissionCommand = new Command(OnAddMission);
            current = null;
            fillTree();
            if (current != null)
            {
                if (current.data is Purpose purp) _selectedAim = purp;
                SubscribeCircles(current);
            }
        }

        public async void SubscribeCircles(Node<abstract_Item> circle)
        {
            if (circle == null) return;
            circles.Add(circle.circle);
            service.Subscribe(async (circle, e) =>
            {
                var touchPoint = (e as TouchEventArgs<Point>).EventData;
                var touchX = touchPoint.X;
                var touchY = touchPoint.Y;

                var viewPosition = new Point((circle as Node<abstract_Item>).circle.x, (circle as Node<abstract_Item>).circle.y);
                var navBarHeight = service.GetNavBarHeight();
                var radius = (circle as Circle).Radius;
                var viewX = viewPosition.X;
                var viewY = viewPosition.Y + navBarHeight;

                if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius)
                {
                    await Shell.Current.GoToAsync($"{nameof(AimEditPage)}?{nameof(AimEditViewModel.ItemId)}={current.data.Id}");
                }
            });
            if (circle.parent != null)
            {
                circles.Add(circle.parent.circle);
                Node<abstract_Item> parent = circle.parent;
                service.Subscribe(async (parent, e) =>
                {
                    var touchPoint = (e as TouchEventArgs<Point>).EventData;
                    var touchX = touchPoint.X;
                    var touchY = touchPoint.Y;

                    var viewPosition = new Point((parent as Node<abstract_Item>).circle.x, (parent as Node<abstract_Item>).circle.y);
                    var navBarHeight = service.GetNavBarHeight();
                    var radius = (parent as Circle).Radius;
                    var viewX = viewPosition.X;
                    var viewY = viewPosition.Y + navBarHeight;

                    if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius)
                    {
                        current = parent as Node<abstract_Item>;
                        if (current.data is Purpose purp) _selectedAim = purp;
                        SubscribeCircles(current);
                    }
                });
            }
            foreach (var child in circle.children)
            {
                circles.Add(child.circle);
                service.Subscribe(async (child, e) =>
                {
                    var touchPoint = (e as TouchEventArgs<Point>).EventData;
                    var touchX = touchPoint.X;
                    var touchY = touchPoint.Y;

                    var viewPosition = new Point((child as Node<abstract_Item>).circle.x, (child as Node<abstract_Item>).circle.y);
                    var navBarHeight = service.GetNavBarHeight();
                    var radius = (child as Circle).Radius;
                    var viewX = viewPosition.X;
                    var viewY = viewPosition.Y + navBarHeight;

                    if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius)
                    {
                        current = child as Node<abstract_Item>;
                        if (current.data is Purpose purp) _selectedAim = purp;
                        SubscribeCircles(current);
                    }
                });
            }
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
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        private async void OnAimSelected(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(AimEditPage)}?{nameof(AimEditViewModel.ItemId)}={current.data.Id}");
        }
        private async void OnAddAim(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewAimPage)}?{nameof(NewAimViewModel.group)}={_selectedAim.Children}");
        }
        private async void OnAddMission(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemViewModel.group)}={_selectedAim.Children}");
        }
        static DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        static double xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        static double xamarinHeight = mainDisplayInfo.Height / mainDisplayInfo.Density;

        public double frame_w { get; set; } = xamarinWidth;
        public double frame_h { get; set; } = xamarinHeight / 2;
    }
}
