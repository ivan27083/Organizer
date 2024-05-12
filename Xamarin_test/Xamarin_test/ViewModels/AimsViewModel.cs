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
        public static bool locker = false;
        private Purpose _selectedAim;
        private string text;
        private string description;
        public Circle plus;
        bool showFill = true;
        public Node<abstract_Item> root;
        public static Node<abstract_Item> current;
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
                if (current.data is Purpose purp)
                {
                    _selectedAim = purp;
                }
            }
            CreateCircles();
            SubscribeCircles(current);
        }
        public void CreateCircles()
        {
            circles.Clear();
            double xamarinWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double xamarinHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            if (current != null)
            {
                current.circle = new Circle
                {
                    x = (float)xamarinWidth / 2,
                    y = (float)xamarinHeight / 2,
                    Radius = 80 / (float)DeviceDisplay.MainDisplayInfo.Density,
                    Type = 0
                };
                circles.Add(current.circle);
                if (current.parent != null)
                {
                    current.parent.circle = new Circle
                    {
                        x = (float)xamarinWidth / 2,
                        y = (float)(xamarinHeight / 2 + xamarinHeight / 5),
                        Radius = 50 / (float)DeviceDisplay.MainDisplayInfo.Density,
                        Type = 1
                    };
                    circles.Add(current.parent.circle);
                }

                foreach (Node<abstract_Item> child in current.children)
                {
                    double radius = Math.Min(xamarinWidth, xamarinHeight) / 4;
                    double angle = current.children.IndexOf(child) * Math.PI / 4;
                    child.circle = new Circle
                    {
                        x = (float)(current.circle.x + radius * Math.Cos(angle)),
                        y = (float)(current.circle.y + radius * Math.Sin(angle)),
                        Radius = (float)(50 - (current.children.Count * 2)) / (float)DeviceDisplay.MainDisplayInfo.Density,
                        Type = 2
                    };
                    circles.Add(child.circle);
                }
            }
            else
            {
                plus = new Circle
                {
                    x = (float)(xamarinWidth / 2),
                    y = (float)(xamarinHeight / 2),
                    Radius = 80 / (float) DeviceDisplay.MainDisplayInfo.Density
                };
            }
        }
        public async void SubscribeCircles(Node<abstract_Item> circle)
        {
            if (circle == null)
            {
                service.Subscribe((sender, e) =>
                {
                    var touchPoint = (e as TouchEventArgs<Point>).EventData;
                    var touchX = touchPoint.X;
                    var touchY = touchPoint.Y;

                    var navBarHeight = service.GetNavBarHeight();
                    var viewX = plus.x;
                    var viewY = plus.y;

                    if (Math.Abs(touchX - viewX) <= plus.Radius && Math.Abs(touchY - viewY) <= plus.Radius && !locker)
                    {
                        locker = true;
                        Shell.Current.GoToAsync($"{nameof(NewAimPage)}?{nameof(NewAimViewModel.Group)}={-1}");
                    }
                });
            }
            else
            {
                Text = current.data.Text;
                Description = current.data.Description;
                service.Subscribe((sender, e) =>
                {
                    var touchPoint = (e as TouchEventArgs<Point>).EventData;
                    var touchX = touchPoint.X;
                    var touchY = touchPoint.Y;

                    var viewPosition = new Point(current.circle.x, current.circle.y);
                    var navBarHeight = service.GetNavBarHeight();
                    var radius = current.circle.Radius;
                    var viewX = viewPosition.X;
                    var viewY = viewPosition.Y;

                    if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius && !locker)
                    {
                        locker = true;
                        Shell.Current.GoToAsync($"{nameof(AimEditPage)}?{nameof(AimEditViewModel.ItemId)}={current.data.Id}");

                    }
                });
                if (circle.parent != null)
                {
                    Node<abstract_Item> parent = circle.parent;
                    service.Subscribe((sender, e) =>
                    {
                        var touchPoint = (e as TouchEventArgs<Point>).EventData;
                        var touchX = touchPoint.X;
                        var touchY = touchPoint.Y;

                        var viewPosition = new Point(current.parent.circle.x, current.parent.circle.y);
                        var navBarHeight = service.GetNavBarHeight();
                        var radius = current.parent.circle.Radius;
                        var viewX = viewPosition.X;
                        var viewY = viewPosition.Y;

                        if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius)
                        {
                            current = current.parent;
                            if (current.data is Purpose purp)
                            {
                                _selectedAim = purp;
                                CreateCircles();
                                SubscribeCircles(current);
                            }
                            
                        }
                    });
                }
                foreach (var child in circle.children)
                {
                    service.Subscribe((sender, e) =>
                    {
                        var touchPoint = (e as TouchEventArgs<Point>).EventData;
                        var touchX = touchPoint.X;
                        var touchY = touchPoint.Y;

                        var viewPosition = new Point(child.circle.x, child.circle.y);
                        var navBarHeight = service.GetNavBarHeight();
                        var radius = child.circle.Radius;
                        var viewX = viewPosition.X;
                        var viewY = viewPosition.Y;

                        if (Math.Abs(touchX - viewX) <= radius && Math.Abs(touchY - viewY) <= radius)
                        {
                            
                            if (child.data is Purpose purp)
                            {
                                current = child;
                                _selectedAim = purp;
                                CreateCircles();
                                SubscribeCircles(current);
                            }
                            else if (child.data is Mission miss)
                            {
                                locker = true;
                                Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={child.data.Id}");
                            }
                        }
                    });
                }
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
            void fillPurposes(List<Purpose> purposes)
            {
                if (purposes.Count == 0) return;
                var el = purposes.Where(p => p.Group == current.data.Id).ToList();
                if (el.Any())
                {
                    foreach (var item in el)
                    {
                        current.AddChild(new Node<abstract_Item>(item));
                        purposes.Remove(item);
                        current = current.children.Last();
                        fillPurposes(purposes);
                    }
                }
                else
                {
                    if (current.parent != null)
                        current = current.parent;
                }
            }
            fillPurposes(purposes);
            current = root;
            void fillMissions(List<Mission> missions)
            {
                var el = missions.Where(m => m.Group == current.data.Id).ToList();
                if (el.Any())
                {
                    foreach (var item in el.ToList())
                    {
                        current.AddChild(new Node<abstract_Item>(item));
                        missions.Remove(item);
                    }
                    current = current.parent;
                    
                }
                else
                {
                    if (current.children.Count != 0)
                        foreach (Node<abstract_Item> item in current.children.ToList())
                        {
                            Node<abstract_Item> temp = item;
                            current = temp;
                            fillMissions(missions);
                        }
                }
            }
            fillMissions(missions);
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
            if (current != null)
                await Shell.Current.GoToAsync($"{nameof(NewAimPage)}?{nameof(NewAimViewModel.Group)}={current.data.Id}");
            else
                await Shell.Current.GoToAsync($"{nameof(NewAimPage)}?{nameof(NewAimViewModel.Group)}={-1}");
        }
        private async void OnAddMission(object obj)
        {
            if (current != null)
                await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemViewModel.group)}={current.data.Id}");
        }
        static DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        static double xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        static double xamarinHeight = mainDisplayInfo.Height / mainDisplayInfo.Density;

        public double frame_w { get; set; } = xamarinWidth;
        public double frame_h { get; set; } = xamarinHeight / 2;
    }
}
